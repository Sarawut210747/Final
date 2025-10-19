using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TimeController timeController;
    [SerializeField] private EconomyController economy;
    [SerializeField] private TenantManager tenantManager;

    [Header("UI")]
    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text tenantsText;

    void Update()
    {
        if (dateText) dateText.text = $"Day {timeController.Day} / M{timeController.Month} / Y{timeController.Year}";
        if (moneyText) moneyText.text = $"${economy.Money:n0}";
        if (tenantsText) tenantsText.text = $"Tenants: {tenantManager.ActiveTenantCount()}";
    }

    // ปุ่มบนมือถือ
    public void OnSpeed(int s) { timeController.SetSpeed(s); }
}
