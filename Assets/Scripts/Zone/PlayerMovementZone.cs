using UnityEngine;

public class PlayerMovementZone : MonoBehaviour, IZoneEffect
{
    [SerializeField] private PlayerData m_playerData;
    private PlayerData m_playerDataBeforeEnter;
    public void Apply(PlayerController controller)
    {
        m_playerDataBeforeEnter = controller.playerData;
        controller.playerData = m_playerData;
    }

    public void Unapply(PlayerController controller)
    {
        controller.playerData = m_playerDataBeforeEnter;
    }
}
