using UnityEngine;

public class EconomyController : MonoBehaviour
{
    public int Money { get; private set; }

    [Header("Refs")]
    [SerializeField] private TimeController timeController;
    [SerializeField] private TenantManager tenantManager;

    void Awake() { Money = SaveService.LoadMoney(300); }
    void OnEnable()
    {
        if (timeController != null) timeController.OnMonthEnded += OnMonthEnded;
    }
    void OnDisable()
    {
        if (timeController != null) timeController.OnMonthEnded -= OnMonthEnded;
    }

    void OnMonthEnded()
    {
        int income = tenantManager.CalculateMonthlyIncome();
        Money += income;
        SaveService.SaveMoney(Money);
        SaveService.Flush();
        Debug.Log($"[Economy] Month ended, income {income}, total {Money}");
        tenantManager.OnMonthlyTick_Contracts();
    }

    public bool TrySpend(int amount)
    {
        if (Money < amount) return false;
        Money -= amount;
        SaveService.SaveMoney(Money);
        return true;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        SaveService.SaveMoney(Money);
    }
}
