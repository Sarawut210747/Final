using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public enum Speed { Pause = 0, x1 = 1, x2 = 2, x4 = 4 }
    [SerializeField] private Speed speed = Speed.x1;
    [SerializeField] private int daysInMonth = 30;
    [SerializeField] private float secondsPerInGameDay = 5f;

    public int Day { get; private set; } = 1;
    public int Month { get; private set; } = 1;
    public int Year { get; private set; } = 1;

    public event Action OnDayPassed;
    public event Action OnMonthEnded;

    float _accum;

    void Start()
    {
        var d = SaveService.LoadDate((1, 1, 1));
        Day = d.day; Month = d.month; Year = d.year;
    }

    void Update()
    {
        if (speed == Speed.Pause) return;
        _accum += Time.deltaTime * (int)speed;

        if (_accum >= secondsPerInGameDay)
        {
            _accum = 0f;
            AdvanceOneDay();
        }
    }

    void AdvanceOneDay()
    {
        Day++;
        OnDayPassed?.Invoke();
        if (Day > daysInMonth)
        {
            Day = 1; Month++;
            if (Month > 12) { Month = 1; Year++; }
            OnMonthEnded?.Invoke();
        }
        SaveService.SaveDate(Day, Month, Year);
    }

    public void SetSpeed(int s)
    {
        speed = (Speed)Mathf.Clamp(s, 0, 4);
    }
}
