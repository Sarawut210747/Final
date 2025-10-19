using System;

public static class IdGenerator
{
    public static string NewId(string prefix = "ID")
        => $"{prefix}_{DateTime.UtcNow.Ticks}_{UnityEngine.Random.Range(1000, 9999)}";
}
