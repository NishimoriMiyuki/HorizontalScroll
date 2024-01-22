using UnityEngine;

public class ThingController : MonoBehaviour
{
    [SerializeField]
    private ScratchHandler _scratchHandler;

    public bool IsScratching => _scratchHandler.IsDragging;
}
