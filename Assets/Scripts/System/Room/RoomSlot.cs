using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RoomSlot : MonoBehaviour
{
    [Header("Room State")]
    public RoomType currentRoomType;
    public NPCData currentNPC;

    [Header("Timing")]
    public float monthDurationSeconds = 10f;
    private Coroutine incomeRoutine;

    [Header("Events")]
    public UnityEvent<RoomSlot> OnRoomBecameEmpty;

    private void Start()
    {
        if (currentRoomType != null)
        {
            StartIncomeCycle();
        }
    }
    public bool IsOccupied => currentNPC != null && currentRoomType != null;
    public bool CanPlaceRoom => currentRoomType == null;

    public void PlaceRoom(RoomType roomType)
    {
        currentRoomType = roomType;
        TrySpawnNPC();
        StartIncomeCycle();
    }

    public void RemoveRoom()
    {
        StopIncomeCycle();
        currentRoomType = null;
        currentNPC = null;
        OnRoomBecameEmpty?.Invoke(this);
    }

    public void UpgradeRoom(RoomType newRoomType)
    {
        currentRoomType = newRoomType;
    }

    public void TrySpawnNPC()
    {
        if (currentNPC != null || currentRoomType == null) return;
        currentNPC = new NPCData() { npcName = "Guest" + Random.Range(1, 9999), happiness = 50, preferredRoomType = currentRoomType.typeName };
    }

    private void StartIncomeCycle()
    {
        if (incomeRoutine != null) StopCoroutine(incomeRoutine);
        incomeRoutine = StartCoroutine(IncomeCycle());
    }

    private void StopIncomeCycle()
    {
        if (incomeRoutine != null) StopCoroutine(incomeRoutine);
        incomeRoutine = null;
    }

    private IEnumerator IncomeCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(monthDurationSeconds);
            ResolveMonth();
        }
    }

    private void ResolveMonth()
    {
        if (!IsOccupied)
        {
            if (currentRoomType != null) TrySpawnNPC();
            return;
        }

        PlayerWallet.Instance.AddMoney(currentRoomType.monthlyRent);
        UIManager.Instance?.ShowFloatingText(transform.position, $"+{currentRoomType.monthlyRent}");

        float chance = currentRoomType.stayChance;
        chance *= Mathf.Clamp01(0.5f + currentNPC.happiness / 200f);

        if (Random.value > chance)
        {
            currentNPC = null;
            UIManager.Instance?.ShowFloatingText(transform.position, "Vacant");
            OnRoomBecameEmpty?.Invoke(this);
            if (currentRoomType != null)
            {
                if (Random.value > 0.5f) TrySpawnNPC();
            }
        }

        UIManager.Instance?.UpdateMoneyDisplay(PlayerWallet.Instance.Money);
    }
}
