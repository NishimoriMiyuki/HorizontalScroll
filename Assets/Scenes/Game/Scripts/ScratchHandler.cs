using UnityEngine;
using UnityEngine.EventSystems;

public class ScratchHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private int _dragCount = 0;
    private float _distance = 0f;
    private Vector2 _startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _distance = Vector2.Distance(_startPosition, eventData.position);

        if (_distance >= 300f)
        {
            _dragCount++;
        }
        _distance = 0f;

        Debug.Log("DragCount = " + _dragCount);
    }
}