using UnityEngine;

public class ProjectileWithParticleController : ProjectileController
{
    protected override bool CanHit(Collider other)
    {
        return other.tag != owner.tag && (other.tag == EnemyData.TAG || other.tag == PlayerData.TAG);
    }

    public void SyncTrigger(Vector3 position, float size)
    {
        _collider.size = Vector3.one * size;
        _collider.center = owner.transform.InverseTransformPoint(position) + owner.transform.up * size * 0.5f;
    }
}