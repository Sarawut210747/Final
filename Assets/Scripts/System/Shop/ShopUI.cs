using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("References")]
    public ShopManager shopManager;
    public RoomSlot selectedSlot;

    public void BuySelectedSlot(RoomType type)
    {
        if (selectedSlot == null)
        {
            Debug.Log("No slot selected!");
            return;
        }
        shopManager.BuyRoomAtSlot(selectedSlot, type);
    }

    public void UpgradeSelectedSlot(RoomType type)
    {
        if (selectedSlot == null)
        {
            Debug.Log("No slot selected!");
            return;
        }
        shopManager.UpgradeRoomAtSlot(selectedSlot, type);
    }

    public void SetSelectedSlot(RoomSlot slot)
    {
        selectedSlot = slot;
        Debug.Log($"Selected slot: {slot.name}");
    }
}
