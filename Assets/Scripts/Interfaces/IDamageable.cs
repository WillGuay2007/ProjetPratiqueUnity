using UnityEngine;

public interface IDamageable
{
    public float health { get; set; }
    void Damage(float damage);
}
