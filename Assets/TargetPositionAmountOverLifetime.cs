using System;
using Unity.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class TargetPositionAmountOverLifetime : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    ParticleSystem _particleSystem;
    [SerializeField]
    AnimationCurve _positionAmountOverLifeTime;
    [SerializeField]
    Transform _target;
#pragma warning restore 649

    ParticleSystem.Particle[] _particles;

    void LateUpdate()
    {
        if (_target == null)
            return;
        
        if (_particles == null)
            _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];

        int particlesCount = _particleSystem.GetParticles(_particles, _particles.Length);
        Vector3 targetParticlePosition = transform.InverseTransformPoint(_target.position);

        for (int i = 0; i < particlesCount; i++)
        {
            ParticleSystem.Particle particle = _particles[i];
            float t = _positionAmountOverLifeTime.Evaluate(1f - particle.remainingLifetime / particle.startLifetime);
            _particles[i].position = Vector3.Lerp(particle.position, targetParticlePosition, t);
        }
        
        _particleSystem.SetParticles(_particles, particlesCount);
    }
}
