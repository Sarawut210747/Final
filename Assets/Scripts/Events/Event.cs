using UnityEngine;

public class Event : MonoBehaviour
{
    public string eventName;

    public int duration = 0; // นับเป็นวัน
    public int maxDuration; // จำนวนวันที่ต้องการให้เกิด event

    public GameObject eventPrefab;

    /*public virtual void Start()
    {
        
    }*/
    public virtual void DayPassed()
    {
        duration++;
    }
}
