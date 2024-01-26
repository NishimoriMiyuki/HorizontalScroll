using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ScratchHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool _isDragging;
    public bool IsDragging => _isDragging;

    private float _distance = 0f;
    private Vector2 _startPosition;

    private const float MIN_DISTANCE = 250f;

    private Subject<Unit> _onPointerDownSubject = new Subject<Unit>();
    private Subject<Unit> _onPointerUpSubject = new Subject<Unit>();

    public IObservable<Unit> OnPointerDownAsObservable() => _onPointerDownSubject;
    public IObservable<Unit> OnPointerUpAsObservable() => _onPointerUpSubject;
    private IDisposable _disposable;

    private void Awake()
    {
        // 1秒間隔で長押しを検知
        this.OnPointerDownAsObservable()
            .SelectMany(_ => Observable.Interval(TimeSpan.FromSeconds(0.5f)))
            .TakeUntil(this.OnPointerUpAsObservable())
            .DoOnCompleted(() => { })
            .RepeatUntilDestroy(this)
            .Subscribe(time =>
            {
                if (time == 0)
                {
                    GameManager.Instance.CatSleep();
                }
            });

        // OnPinterUpが発生してから1秒たったらCatSleepする
        this.OnPointerUpAsObservable()
            .Subscribe(_ => {
                _disposable?.Dispose();
                _disposable = Observable.Timer(TimeSpan.FromSeconds(0.5f))
                .Do(_ => GameManager.Instance.CatSleep())
                .Subscribe();
            });
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _startPosition = eventData.position;

        GameManager.Instance.CatNailSharpener();
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _distance = Vector2.Distance(_startPosition, eventData.position);

        if (_distance >= MIN_DISTANCE)
        {
            GameUIManager.Instance.DragCount();
        }
        _distance = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _onPointerDownSubject.OnNext(Unit.Default);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _onPointerUpSubject.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _onPointerDownSubject.OnCompleted();
        _onPointerUpSubject.OnCompleted();
        _disposable?.Dispose();
    }
}