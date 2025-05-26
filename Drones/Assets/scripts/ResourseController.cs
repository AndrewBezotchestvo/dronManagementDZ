using UnityEngine;

public class ResourseController : MonoBehaviour
{

    private bool _available;

    private void OnEnable()
    {
        _available = true;
    }

    public bool GetAvailable()
    {
        return _available;
    }

    public void SetDron()
    {
        _available = false;
    }
}
