using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public RoomType[] availbleRoomTypes;

    public void BuyRoomAtSlot(RoomSlot slot, RoomType roomType)
    {
        if (!slot.CanPlaceRoom) { Debug.Log("Slot occupied"); return; }
        if (PlayerWallet.Instance.TrySpend(roomType.buildCost))
        {
            slot.PlaceRoom(roomType);
            UIManager.Instance?.UpdateMoneyDisplay(PlayerWallet.Instance.Money);
            Debug.Log($"Placed {roomType.typeName} in slot {slot.name}");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }


    public void UpgradeRoomAtSlot(RoomSlot slot, RoomType targetType)
    {
        if (slot.currentRoomType == null) return;
        if (PlayerWallet.Instance.TrySpend(targetType.upgradeCost))
        {
            slot.UpgradeRoom(targetType);
            UIManager.Instance?.UpdateMoneyDisplay(PlayerWallet.Instance.Money);
        }
    }
}