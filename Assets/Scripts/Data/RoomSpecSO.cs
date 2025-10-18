using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpec", menuName = "IdleHostel/RoomSpec")]
public class RoomSpecSO : ScriptableObject
{
    [Header("Info")]
    public string roomName = "Standard";
    public bool isVIP = false;
    public Sprite icon;
    public GameObject prefab;

    [Header("Economy")]
    public int basePrice = 1000;
    public int baseIncome = 200;
    [Tooltip("คูณรายได้ตามเลเวล (x-axis: level, y-axis: multiplier)")]
    public AnimationCurve incomeByLevel = AnimationCurve.Linear(1, 1, 5, 2f);

    [Header("Upgrade")]
    public int baseUpgradeCost = 500;
    [Tooltip("คูณราคาอัปเกรดตามเลเวล (x-axis: level, y-axis: multiplier)")]
    public AnimationCurve upgradeCostByLevel = AnimationCurve.Linear(1, 1, 5, 3f);
}
