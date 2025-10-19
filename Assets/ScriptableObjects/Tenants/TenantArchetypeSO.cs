using UnityEngine;

[CreateAssetMenu(menuName = "Dormitory/Tenant Archetype", fileName = "TenantArchetypeSO")]
public class TenantArchetypeSO : ScriptableObject
{
    [Header("Budget & Contract")]
    public Vector2Int monthlyBudgetRange = new Vector2Int(50, 140);
    public Vector2Int contractMonthsRange = new Vector2Int(1, 3);

    [Header("Personality")]
    [Range(0f, 1f)] public float picky;
    [Range(0f, 1f)] public float tidyLove;
    [Range(0f, 1f)] public float noiseHate;

    public int RollBudget() => Random.Range(monthlyBudgetRange.x, monthlyBudgetRange.y + 1);
    public int RollContractMonths() => Random.Range(contractMonthsRange.x, contractMonthsRange.y + 1);
}
