using UnityEngine;

public class ParticleSyncDeathController : MonoBehaviour
{
    public GameObject root = null;

    private void OnParticleSystemStopped()
    {
        root.SetActive(false);
    }
}