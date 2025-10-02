using UnityEngine;

[CreateAssetMenu(fileName = "New RoomType", menuName = "IdleDorm/RoomType")]
public class RoomType : ScriptableObject
{
    public string typeName;
    public Sprite icon;
    public int buildCost = 100;
    public int upgradeCost = 150;
    public int monthlyRent = 100;
    [Range(0f, 1f)] public float stayChance = 0.8f; // probability NPC stays after month
}
