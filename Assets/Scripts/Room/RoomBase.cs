using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RoomBase : MonoBehaviour, IIncomeSource, IUpgradeable
{
    [SerializeField] private RoomSpecSO spec;
    [SerializeField] private int level = 1;
    [SerializeField] private SpriteRenderer sr;

    public RoomSpecSO Spec => spec;
    public int Level => level;

    private void Reset()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(RoomSpecSO s)
    {
        spec = s;
        if (!sr) sr = GetComponent<SpriteRenderer>();
    }

    // -------- IIncomeSource --------
    public int GetIncomePerMonth()
    {
        if (!spec) return 0;
        var mult = Mathf.Max(0.01f, spec.incomeByLevel.Evaluate(level));
        return Mathf.RoundToInt(spec.baseIncome * mult);
    }

    // -------- IUpgradeable --------
    public bool CanUpgrade(int playerMoney) => spec && playerMoney >= GetUpgradeCost();

    public int GetUpgradeCost()
    {
        if (!spec) return int.MaxValue;
        var mult = Mathf.Max(0.01f, spec.upgradeCostByLevel.Evaluate(level));
        return Mathf.RoundToInt(spec.baseUpgradeCost * mult);
    }

    public void Upgrade()
    {
        level = Mathf.Clamp(level + 1, 1, 99);
    }
}
