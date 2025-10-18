using System.Collections.Generic;

public class CompositeValidator : IPlaceableValidator
{
    private readonly List<IPlaceableValidator> validators = new();
    private string reason = "";
    public string Reason => reason;

    public CompositeValidator Add(IPlaceableValidator v)
    {
        validators.Add(v);
        return this;
    }

    public bool CanPlace(RoomSlot slot)
    {
        foreach (var v in validators)
        {
            if (!v.CanPlace(slot))
            {
                reason = v.Reason;
                return false;
            }
        }
        reason = "";
        return true;
    }
}
