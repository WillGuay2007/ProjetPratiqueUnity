using UnityEngine;

public interface IZoneEffect
{
    public void Apply(PlayerController controller);
    public void Unapply(PlayerController controller);
}
