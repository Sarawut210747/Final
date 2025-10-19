using System;

[Serializable]
public class Tenant
{
    public string TenantId;
    public string RoomId;
    public int MonthlyRent;
    public int MonthsLeft;
    public float Satisfaction;

    public bool IsActive => MonthsLeft > 0 && !string.IsNullOrEmpty(RoomId);
}
