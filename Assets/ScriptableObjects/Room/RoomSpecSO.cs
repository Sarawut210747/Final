using UnityEngine;

[CreateAssetMenu(menuName = "Dormitory/Room Spec", fileName = "RoomSpecSO")]
public class RoomSpecSO : ScriptableObject
{
    [Header("Identity")]
    public string roomName;
    public Sprite icon;

    [Header("Economy")]
    public int buildCost = 100;
    public int upgradeBaseCost = 80;
    public int maxLevel = 3;

    [Header("Effects (per level)")]
    public int[] baseMonthlyRentPerLevel = new int[] { 70, 90, 130 };
    public float[] baseSatisfactionPerLevel = new float[] { 0.0f, 0.05f, 0.1f };

    public int GetMonthlyRent(int level)
        => baseMonthlyRentPerLevel[Mathf.Clamp(level - 1, 0, baseMonthlyRentPerLevel.Length - 1)];

    public float GetSatisfactionBonus(int level)
        => baseSatisfactionPerLevel[Mathf.Clamp(level - 1, 0, baseSatisfactionPerLevel.Length - 1)];

    public int GetUpgradeCost(int toLevel)
        => Mathf.RoundToInt(upgradeBaseCost * Mathf.Pow(1.35f, toLevel - 2));
}
