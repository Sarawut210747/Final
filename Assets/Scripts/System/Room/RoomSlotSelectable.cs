using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RoomSlot))]
public class RoomSlotSelectable : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Reference to ShopUI; if left empty will auto-find on first click")]
    public ShopUI shopUI;


    private RoomSlot slot;


    private void Awake()
    {
        slot = GetComponent<RoomSlot>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (shopUI == null)
            shopUI = FindFirstObjectByType<ShopUI>();

        if (shopUI == null)
        {
            Debug.LogWarning("ShopUI not found in scene");
            return;
        }

        shopUI.SetSelectedSlot(slot);
    }
}
