using UnityEngine;

public class ParticleSyncDeathController : MonoBehaviour
{
    public ParticleSystem system = null;

    private void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }
}