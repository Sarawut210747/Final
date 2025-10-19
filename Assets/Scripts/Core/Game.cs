using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Core")]
    public TimeController timeController;
    public EconomyController economyController;
    public BuildingGrid buildingGrid;
    public TenantManager tenantManager;
    public EventManager eventManager;
    public HUDController hud;

    [Header("Content")]
    public RoomSpecSO starterRoom;

    void Start()
    {
        var empty = buildingGrid.GetFirstEmptySlot();
        if (empty != null && starterRoom != null)
        {
            empty.Build(economyController, starterRoom);
            tenantManager.FillVacancies();
        }
    }
}
