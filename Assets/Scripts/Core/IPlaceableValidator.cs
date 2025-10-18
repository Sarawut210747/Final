using UnityEngine;

public interface IPlaceableValidator
{
    bool CanPlace(RoomSlot slot);
    string Reason { get; }
}
