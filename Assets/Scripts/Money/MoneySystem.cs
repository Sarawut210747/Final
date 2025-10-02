using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance;
    [Header("Money settings")]
    public int currentMoney;

    [Header("UI")]
    public TMP_Text moneyText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        Debug.Log("ได้เงินเพิ่ม : " + amount);
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            Debug.Log("ใช้เงิน:" + amount);
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("เงินไม่พอ");
            return false;
        }
    }

    private void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = currentMoney.ToString();
        }
    }
}
