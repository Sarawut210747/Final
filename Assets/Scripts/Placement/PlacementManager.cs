using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager I;

    [Header("Refs")]
    public Camera mainCam;
    public List<RoomSlot> allSlots = new();

    [Header("Preview")]
    public GameObject previewPrefab; // GO ว่างมี RoomPreview + SpriteRenderer
    private RoomPreview activePreview;

    private RoomSpecSO pendingSpec;
    private RoomSlot hoverSlot;

    private bool placingMode = false;
    private IPlaceableValidator validator;

    private void Awake()
    {
        I = this;
        if (!mainCam) mainCam = Camera.main;

        // รวมเงื่อนไขได้หลายตัวในอนาคต (เช่น ระยะ, เขตก่อสร้าง, โซนไฟ)
        validator = new CompositeValidator()
            .Add(new SupportBelowValidator());
    }

    private void Update()
    {
        if (!placingMode || activePreview == null) return;

        // กันกดทะลุ UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            activePreview.SetValid(false);
            return;
        }

        bool tap;
        Vector3 screenPos;

#if UNITY_EDITOR
        tap = Input.GetMouseButtonDown(0);
        screenPos = Input.mousePosition;
#else
        tap = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        screenPos = (Input.touchCount > 0) ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;
#endif

        Vector3 worldPos = mainCam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        hoverSlot = FindNearestFreeSlot(worldPos, 1.2f);
        bool can = hoverSlot != null && validator.CanPlace(hoverSlot);

        if (hoverSlot != null)
            activePreview.transform.position = hoverSlot.GetSnapPosition();
        else
            activePreview.transform.position = worldPos;

        activePreview.SetValid(can);

        if (tap && can)
        {
            hoverSlot.Place(pendingSpec);
            ExitPlacingMode();
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            ExitPlacingMode();
    }

    public void EnterPlacingMode(RoomSpecSO spec)
    {
        pendingSpec = spec;
        placingMode = true;

        if (activePreview != null) Destroy(activePreview.gameObject);

        var go = Instantiate(previewPrefab, Vector3.zero, Quaternion.identity, transform);
        activePreview = go.GetComponent<RoomPreview>();

        // ดึงสไปรต์จาก prefab จริง
        var sprite = spec.prefab.GetComponentInChildren<SpriteRenderer>()?.sprite;
        activePreview.SetSprite(sprite);
        activePreview.SetValid(false);

        // โชว์ Hint เฉพาะช่องที่ยังไม่ถูกยึด
        foreach (var s in allSlots) s.ShowHint(!s.isOccupied);
    }

    public void ExitPlacingMode()
    {
        placingMode = false;
        pendingSpec = null;
        if (activePreview) Destroy(activePreview.gameObject);
        activePreview = null;
        hoverSlot = null;

        foreach (var s in allSlots) s.ShowHint(false);
    }

    private RoomSlot FindNearestFreeSlot(Vector3 worldPos, float maxDist)
    {
        RoomSlot best = null;
        float bestD = float.MaxValue;
        foreach (var s in allSlots)
        {
            if (s.isOccupied) continue;
            float d = Vector3.Distance(worldPos, s.transform.position);
            if (d < bestD && d <= maxDist)
            {
                best = s;
                bestD = d;
            }
        }
        return best;
    }
}
