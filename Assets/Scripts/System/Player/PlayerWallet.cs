using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }

    public int Money = 500;

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        OnMoneyChanged?.Invoke(Money);
    }

    public bool TrySpend(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            OnMoneyChanged?.Invoke(Money);
            return true;
        }
        return false;
    }
}
