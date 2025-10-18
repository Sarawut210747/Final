public class SupportBelowValidator : IPlaceableValidator
{
    private string reason = "";
    public string Reason => reason;

    public bool CanPlace(RoomSlot slot)
    {
        if (slot.isOccupied) { reason = "ช่องนี้มีห้องอยู่แล้ว"; return false; }
        if (!slot.requireSupportFromBelow) return true;
        if (slot.floorIndex == 0) return true;
        if (slot.slotBelow == null || !slot.slotBelow.isOccupied)
        {
            reason = "ต้องมีห้องรองรับจากชั้นล่าง";
            return false;
        }
        return true;
    }
}
