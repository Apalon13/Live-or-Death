using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public bool Targetable { set; get; }
    public bool Invincible { set; get; }
    public void OnHit(float damage, GameObject sender);
    public void OnObjectDestroy();
    public void damageText(float damage);
}
