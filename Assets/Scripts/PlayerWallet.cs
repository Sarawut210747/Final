using UnityEngine;
using TMPro;

public class PlayerWallet : MonoBehaviour
{
    public int currentMoney = 1000;

    [Header("Optional UI")]
    [SerializeField] private TMP_Text moneyText;

    private void Start() => RefreshUI();

    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            RefreshUI();
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (moneyText) moneyText.text = $"${currentMoney}";
    }
}
