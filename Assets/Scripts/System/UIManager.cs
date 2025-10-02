using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI moneyText;
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {

        int startMoney = PlayerWallet.Instance != null ? PlayerWallet.Instance.Money : 0;
        UpdateMoneyDisplay(startMoney);
    }

    public void UpdateMoneyDisplay(int money)
    {
        if (moneyText == null) return;
        moneyText.text = FormatMoney(money);
    }

    private string FormatMoney(int money)
    {
        return $"{money:N0}";
    }

    public void ShowFloatingText(Vector3 worldPos, string text)
    {
        Debug.Log($"FloatingText @ {worldPos}: {text}");
    }
    private void OnEnable()
    {
        if (PlayerWallet.Instance != null)
            PlayerWallet.Instance.OnMoneyChanged += UpdateMoneyDisplay;
    }

    private void OnDisable()
    {
        if (PlayerWallet.Instance != null)
            PlayerWallet.Instance.OnMoneyChanged -= UpdateMoneyDisplay;
    }
}
