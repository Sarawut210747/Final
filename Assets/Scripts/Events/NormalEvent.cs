using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class NormalEvent : Event
{
    public bool isInteract;
    public RoomInstance roomInstance;


    public NormalEvent(RoomInstance room)
    {
        roomInstance = room;
    }
    private void Start()
    {
        StartCoroutine(TimerCoroutine());
        CreateEvent();

    }
    IEnumerator TimerCoroutine()
    {
        while (duration < maxDuration)
        {
            if (isInteract) // กด แก้ไข event
            {
                AddReputation();

                yield break;
            }
            yield return null;
        }
        // ไม่กด แก้ไข event
        ReduceReputation(); // ! ลดคร้งเดียว

    }

    /*public override void DayPassed() // ลด rep หลายครั้ง จนกว่าจะกด
    {
        base.DayPassed();
        if (duration >= maxDuration && !isInteract)
        {
            ReduceReputation();
        }
    }*/

    // สร้าง obj event
    public void CreateEvent()
    {
        // สร้าง event ไว้ที่ห้อง  !! re-check ref ห้อง !!
        GameObject eventObj = Instantiate(eventPrefab, roomInstance.gameObject.transform.position, Quaternion.identity);

        // ต้องมี collider
        if (eventObj.GetComponent<Collider2D>() == null)
        {
            eventObj.AddComponent<BoxCollider2D>();
        }
        // เพิ่ม EventTrigger Component
        EventTrigger trigger = eventObj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = eventObj.AddComponent<EventTrigger>();
        }

        // สร้าง EventTrigger.Entry สำหรับ PointerClick
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;

        // ใส่ Action เวลากดคลิก
        clickEntry.callback.AddListener((eventData) =>
        {
            this.OnNormalEventClicked();
            Destroy(eventObj.gameObject);
        });

        // เพิ่ม entry เข้า EventTrigger
        trigger.triggers.Add(clickEntry);
    }

    // เรียกจาก eventPrefab
    public void OnNormalEventClicked()
    {
        isInteract = true;
        EventManager.GetInstance().RemoveNormalEvent(this);
        Destroy(this);
    }
    

    #region Reputation
    public void AddReputation()
    {
        float rep = 0;
        AddRentCost(); // เพิ่มค่านิยม => เพิ่มรายได้
        float repClamped = ClampReputation(rep);
        // add reputation ตาม repClamped
        // 
    }
    public void ReduceReputation()
    {

        ReduceRentCost(); // ลดค่านิยม => ลดรายได้
    }
    public float ClampReputation(float rep)
    {
        return 0;
    }
    #endregion

    #region Rent Cost
    public void AddRentCost()
    {

    }
    public void ReduceRentCost()
    {

    }
    public float ClampRentCost(float rentCost)
    {
        return 0;
    }
    #endregion

    public void TenantLeave()
    {
        
    }
}

