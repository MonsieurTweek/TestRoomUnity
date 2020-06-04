using System.Collections.Generic;
using UnityEngine;

public class ParticleSyncShapeController : MonoBehaviour
{
    public ParticleSystem system = null;
    public ProjectileWithParticleController projectile = null;

    private List<ParticleSystem.Particle> _particles = new List<ParticleSystem.Particle>();
    private Vector3 _averagePosition = Vector3.zero;
    private float _averageSize = 0f;

    private void Awake()
    {
        _averagePosition = transform.position;
        _averagePosition.y = 0f;
    }

    private void OnParticleTrigger()
    {
        int nbParticles = system.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, _particles);

        if (nbParticles > 0)
        {
            float posX = 0f;
            float posZ = 0f;
            float size = 0f;

            // iterate through the particles which entered the trigger and make them red
            for (int i = 0; i < nbParticles; i++)
            {
                ParticleSystem.Particle particule = _particles[i];

                posX += particule.position.x;
                posZ += particule.position.z;
                size += particule.GetCurrentSize(system);

                _particles[i] = particule;
            }

            _averagePosition.x = posX / nbParticles;
            _averagePosition.z = posZ / nbParticles;
            _averageSize = size / nbParticles;

            projectile.SyncTrigger(_averagePosition, _averageSize);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 1f, 0.3f);
        Gizmos.DrawSphere(_averagePosition, _averageSize);
    }
}