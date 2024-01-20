using UnityEngine;
using UnityEngine.UI;

public class CatDegree : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public void Init(int requiredNumberScratches)
    {
        _slider.maxValue = requiredNumberScratches;
        _slider.value = 0;
    }

    public void SliderUpdate(int currentDragNumber)
    {
        _slider.value = currentDragNumber;
    }
}
