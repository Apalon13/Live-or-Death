using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public bool Targetable { set; get; }
    public bool Invincible { set; get; }
    public void OnHit(float damage, Vector2 knockback, string tagEnemy);
    public void OnObjectDestroy();
}
