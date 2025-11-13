using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    private RectTransform _rt;
    private Rect _lastSafe;

    void Awake()
    {
        EnsureRT();
        Apply();
    }

    void OnEnable()
    {
        EnsureRT();
        Apply();
    }

    void OnRectTransformDimensionsChange()
    {
        Apply();
    }

    void EnsureRT()
    {
        if (_rt == null) _rt = GetComponent<RectTransform>();
    }

    void Apply()
    {
        EnsureRT();
        if (_rt == null) return;

        var safe = Screen.safeArea;
        if (safe == _lastSafe) return;
        _lastSafe = safe;

        float w = Mathf.Max(1, Screen.width);
        float h = Mathf.Max(1, Screen.height);

        Vector2 anchorMin = safe.position;
        Vector2 anchorMax = safe.position + safe.size;
        anchorMin.x /= w; anchorMin.y /= h;
        anchorMax.x /= w; anchorMax.y /= h;

        _rt.anchorMin = anchorMin;
        _rt.anchorMax = anchorMax;
    }
}
