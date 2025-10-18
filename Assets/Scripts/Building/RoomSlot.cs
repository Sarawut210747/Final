using UnityEngine;

public class RoomSlot : MonoBehaviour
{
    [Header("State")]
    public bool isOccupied = false;
    public RoomBase placedRoom;

    [Header("Floor/Support")]
    public int floorIndex = 0;
    public RoomSlot slotBelow;
    public bool requireSupportFromBelow = true;

    [Header("Visual Hint")]
    [SerializeField] private SpriteRenderer hintRenderer;
    [SerializeField] private Color hintOn = new(1f, 1f, 1f, 0.35f);
    [SerializeField] private Color hintOff = new(1f, 1f, 1f, 0f);

    private void Reset()
    {
        if (!hintRenderer)
            hintRenderer = transform.Find("Hint")?.GetComponent<SpriteRenderer>();
    }

    public void ShowHint(bool show)
    {
        if (!hintRenderer) return;
        hintRenderer.color = show ? hintOn : hintOff;
    }

    public Vector3 GetSnapPosition() => transform.position;

    public void Place(RoomSpecSO spec)
    {
        if (placedRoom) Clear();
        placedRoom = RoomFactory.CreateRoomInstance(spec, transform, GetSnapPosition());
        isOccupied = true;
        ShowHint(false);
    }

    public void Clear()
    {
        if (placedRoom) Destroy(placedRoom.gameObject);
        placedRoom = null;
        isOccupied = false;
    }
}
