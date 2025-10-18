using UnityEngine;

public static class RoomFactory
{
    public static RoomBase CreateRoomInstance(RoomSpecSO spec, Transform parent, Vector3 pos)
    {
        var go = Object.Instantiate(spec.prefab, pos, Quaternion.identity, parent);
        var room = go.GetComponent<RoomBase>();
        if (!room) room = go.AddComponent<RoomBase>();
        room.Init(spec);
        return room;
    }
}
