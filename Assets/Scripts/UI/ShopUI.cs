using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Refs")]
    public PlayerWallet wallet;
    public RoomSpecSO[] availableRooms;

    [Header("UI")]
    public Transform shopContent;
    public GameObject roomButtonPrefab;

    private void Start()
    {
        foreach (var spec in availableRooms)
        {
            var btnObj = Instantiate(roomButtonPrefab, shopContent);
            var label = btnObj.GetComponentInChildren<TMP_Text>();
            var iconImg = btnObj.transform.Find("Icon")?.GetComponent<Image>();

            if (label) label.text = $"{spec.roomName}\n${spec.basePrice}";
            if (iconImg) iconImg.sprite = spec.icon;

            btnObj.GetComponent<Button>().onClick.AddListener(() => TryBuy(spec));
        }
    }

    private void TryBuy(RoomSpecSO spec)
    {
        if (wallet.SpendMoney(spec.basePrice))
        {
            PlacementManager.I.EnterPlacingMode(spec);
        }
        else
        {
            Debug.Log("เงินไม่พอ");
        }
    }
}
