using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Economy")]
    public PlayerWallet playerWallet;
    public float monthDuration = 30f;
    public List<RoomSlot> roomSlots = new();

    [Header("Tenant Leave Chance")]
    [Range(0f, 1f)] public float leaveChance = 0.3f;

    private void Start()
    {
        StartCoroutine(MonthlyIncomeRoutine());
    }

    private IEnumerator MonthlyIncomeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(monthDuration);

            int totalIncome = 0;
            foreach (var slot in roomSlots)
            {
                if (slot.isOccupied && slot.placedRoom != null)
                {
                    totalIncome += slot.placedRoom.GetIncomePerMonth();
                }
            }
            playerWallet.AddMoney(totalIncome);
            Debug.Log($"[End of Month] Income = {totalIncome}");

            // สุ่มผู้เช่าออก -> ช่องว่าง
            foreach (var slot in roomSlots)
            {
                if (slot.isOccupied && Random.value < leaveChance)
                {
                    slot.Clear();
                }
            }
        }
    }
}
