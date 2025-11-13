using System.Collections.Generic;
using UnityEngine;

public class TenantManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private BuildingGrid buildingGrid;
    [SerializeField] private EconomyController economy;
    [SerializeField] private TenantArchetypeSO defaultArchetype;

    private readonly List<Tenant> _tenants = new();

    public int CalculateMonthlyIncome()
    {
        int sum = 0;
        foreach (var t in _tenants)
        {
            if (!t.IsActive) continue;
            sum += t.MonthlyRent;
        }
        return sum;
    }

    public void OnMonthlyTick_Contracts()
    {

        for (int i = _tenants.Count - 1; i >= 0; i--)
        {
            var t = _tenants[i];
            if (!t.IsActive) continue;

            t.MonthsLeft = Mathf.Max(0, t.MonthsLeft - 1);

            if (t.MonthsLeft == 0)
            {
                bool renew = (t.Satisfaction >= 50f) && (Random.value <= 0.6f);
                if (renew)
                {
                    t.MonthsLeft = defaultArchetype.RollContractMonths();
                }
                else
                {
                    t.RoomId = null;
                }
            }
        }
        FillVacancies();
    }

    public void FillVacancies()
    {
        foreach (var slot in buildingGrid.Slots)
        {
            if (!slot.HasRoom) continue;

            bool occupied = _tenants.Exists(t => t.IsActive && t.RoomId == slot.Current.RoomId);
            if (occupied) continue;

            var arch = defaultArchetype;
            var rent = slot.Current.GetMonthlyRent();
            int budget = arch.RollBudget();

            if (budget >= Mathf.RoundToInt(rent * 0.8f))
            {
                var t = new Tenant
                {
                    TenantId = IdGenerator.NewId("TEN"),
                    RoomId = slot.Current.RoomId,
                    MonthlyRent = rent,
                    MonthsLeft = arch.RollContractMonths(),
                    Satisfaction = 50f + arch.tidyLove * 10f - arch.picky * 10f
                };
                _tenants.Add(t);
            }
        }
    }

    public int ActiveTenantCount()
    {
        int c = 0; foreach (var t in _tenants) if (t.IsActive) c++; return c;
    }
}
