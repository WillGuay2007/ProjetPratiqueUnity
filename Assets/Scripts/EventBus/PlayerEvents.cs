using UnityEngine;

public class PlayerRespawnEvent: IEvent
{
    public Vector3 respawnPosition;
    public PlayerRespawnEvent(Vector3 respawnPosition)
    {
        this.respawnPosition = respawnPosition;
    }
}
