using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Slots")]
    public List<RoomSlot> allSlots = new List<RoomSlot>();

    [Header("Offline/Time")]
    public float realSecondsPerGameMonth = 10f;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        int placed = 0;
        foreach (var s in allSlots)
        {
            if (placed >= 3) break;
            if (s.CanPlaceRoom)
            {
                placed++;
            }
        }
    }
    public RoomSlot GetFirstEmptySlot()
    {
        foreach (var s in allSlots)
            if (s.CanPlaceRoom) return s;
        return null;
    }

}