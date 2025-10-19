using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    public string RoomId { get; private set; }
    public RoomSpecSO Spec { get; private set; }
    public int Level { get; private set; }

    public void Initialize(RoomSpecSO spec, int level)
    {
        RoomId = IdGenerator.NewId("ROOM");
        Spec = spec;
        Level = Mathf.Clamp(level, 1, spec.maxLevel);
        // TODO: อัปเดตรูป/ไอคอน/สกินตาม Level
    }

    public int GetMonthlyRent() => Spec.GetMonthlyRent(Level);

    public bool TryUpgrade(EconomyController eco)
    {
        int target = Mathf.Clamp(Level + 1, 1, Spec.maxLevel);
        if (target == Level) return false;
        int cost = Spec.GetUpgradeCost(target);
        if (!eco.TrySpend(cost)) return false;
        Level = target;
        // TODO: อัปเดตวิชวล
        return true;
    }
}
