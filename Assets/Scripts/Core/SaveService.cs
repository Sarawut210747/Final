using UnityEngine;

public static class SaveService
{
    const string KEY_MONEY = "MONEY";
    const string KEY_DAY = "DAY";
    const string KEY_MONTH = "MONTH";
    const string KEY_YEAR = "YEAR";

    public static void SaveMoney(int value) { PlayerPrefs.SetInt(KEY_MONEY, value); }
    public static int LoadMoney(int defaultValue = 300) { return PlayerPrefs.GetInt(KEY_MONEY, defaultValue); }

    public static void SaveDate(int day, int month, int year)
    {
        PlayerPrefs.SetInt(KEY_DAY, day); PlayerPrefs.SetInt(KEY_MONTH, month); PlayerPrefs.SetInt(KEY_YEAR, year);
    }
    public static (int day, int month, int year) LoadDate((int, int, int) fallback)
    {
        if (!PlayerPrefs.HasKey(KEY_DAY)) return fallback;
        return (PlayerPrefs.GetInt(KEY_DAY), PlayerPrefs.GetInt(KEY_MONTH), PlayerPrefs.GetInt(KEY_YEAR));
    }

    public static void Flush() { PlayerPrefs.Save(); }
}
