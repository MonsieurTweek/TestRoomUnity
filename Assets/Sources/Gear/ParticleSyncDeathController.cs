using UnityEngine;

public class ParticleSyncDeathController : MonoBehaviour
{
    public ParticleSystem system = null;
    public ProjectileWithParticleController projectile = null;

    private void OnParticleSystemStopped()
    {
        projectile.Destroy();
    }
}