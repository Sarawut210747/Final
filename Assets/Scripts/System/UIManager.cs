using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }


    public void UpdateMoneyDisplay(int money)
    {
        // Hook into your UI text field
        Debug.Log("Money: " + money);
    }


    public void ShowFloatingText(Vector3 worldPos, string text)
    {
        // Simple debug placeholder, replace with actual floating text prefab if desired
        Debug.Log($"FloatingText @ {worldPos}: {text}");
    }
}
