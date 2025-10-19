using UnityEngine;

public class RoomSlot : MonoBehaviour
{
    [SerializeField] private Transform anchor;
    [SerializeField] private RoomInstance roomPrefab;

    public RoomInstance Current { get; private set; }
    public bool HasRoom => Current != null;

    public void Setup(RoomInstance instance)
    {
        Current = instance;
    }

    public bool Build(EconomyController eco, RoomSpecSO spec)
    {
        if (HasRoom) return false;
        if (!eco.TrySpend(spec.buildCost)) return false;

        var inst = Instantiate(roomPrefab, anchor);
        inst.Initialize(spec, 1);
        Current = inst;
        return true;
    }

    public bool Upgrade(EconomyController eco)
    {
        if (!HasRoom) return false;
        return Current.TryUpgrade(eco);
    }
}
