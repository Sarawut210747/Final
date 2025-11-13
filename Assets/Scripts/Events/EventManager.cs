using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //
    private List<NormalEvent> activeNormalEvents = new();
    private List<WorldEvent> activeWorldEvents = new();

    private int roomCount;
    #region singleton
    private static EventManager instance;
    public static EventManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    private TimeController timeController;
    public void SetTimeController(TimeController _timeController)
    {
        timeController = _timeController;
    }

    void OnEnable()
    {
        timeController.OnDayPassed += CheckWorldEvents;

        foreach (var n in activeNormalEvents) // นับวันให้ event
        {
            n.DayPassed();
        }
    }

    void OnDisable()
    {
        timeController.OnDayPassed -= CheckWorldEvents;
    }

    //
    void CheckWorldEvents()
    {
        if (Random.value < 0.05f) // โอกาส 5% ต่อวัน
        {
            if (Random.value < 0.5f) // 50/50
            {
                TriggerWorldEvent();
            }
            else
            {
                TriggerNormalEvent();
            }
        }
    }

    void TriggerWorldEvent()
    {
        int roomCountToSetEvent = 3; // จำนวนห้องที่โดน world event
        List<RoomInstance> rooms = new List<RoomInstance>();
        for (int i = 0; i < roomCountToSetEvent; i++)
        {
            RoomInstance room = GetRandomRoomInScene();
            if (room == null)
            {
                return; // no room
            }

            bool hasRoom = false;
            foreach (RoomInstance r in rooms)
            {
                if (room == r)
                {
                    hasRoom = true;
                    break;
                }
            }
            if (!hasRoom)
            {
                rooms.Add(room);
            }
        }

        WorldEvent worldEvent = new WorldEvent(rooms);
        activeWorldEvents.Add(worldEvent);
    }
    void TriggerNormalEvent()
    {
        RoomInstance room = GetRandomRoomInScene();
        if (room == null)
        {
            return; // no room
        }

        NormalEvent normalEvent = new NormalEvent(room);
        activeNormalEvents.Add(normalEvent);
    }

    // get room
    public RoomInstance GetRandomRoomInScene()
    {
        // หาห้องทั้งหมด
        List<RoomInstance> allRoom = new List<RoomInstance>();
        RoomInstance[] findRoom = FindObjectsByType<RoomInstance>(FindObjectsSortMode.None);

        // ตรวจสอบ
        if (findRoom.Length == 0) return null;

        SetRoomCount(findRoom.Length);

        // สุ่มห้อง
        int rand = Random.Range(0, findRoom.Length);
        RoomInstance randomRoom = findRoom[rand];

        return randomRoom;
    }
    private void SetRoomCount(int r)
    {
        roomCount = r;
    }

    //
    public void RemoveNormalEvent(NormalEvent _event)
    {
        activeNormalEvents.Remove(_event);
    }
    public void RemoveWorldEvent(WorldEvent _event)
    {
        activeWorldEvents.Remove(_event);
    }
}
