using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private int rows = 3;
    [SerializeField] private int cols = 4;
    [SerializeField] private RoomSlot slotPrefab;
    [SerializeField] private Transform slotsRoot;

    private List<RoomSlot> _slots = new();

    public IReadOnlyList<RoomSlot> Slots => _slots;

    void Start()
    {
        GenerateSlots();
    }

    void GenerateSlots()
    {
        _slots.Clear();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                var slot = Instantiate(slotPrefab, slotsRoot);
                slot.name = $"Slot_{r}_{c}";
                slot.Setup(null);
                _slots.Add(slot);
            }
        }
    }

    public RoomSlot GetFirstEmptySlot()
    {
        foreach (var s in _slots) if (!s.HasRoom) return s;
        return null;
    }
}
