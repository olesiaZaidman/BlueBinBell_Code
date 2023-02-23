using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    [Header("Particle FX")]
    [SerializeField] ParticleSystem _dustParticle;
    public void CreateDustEffect()
    {
        _dustParticle.Play();
    }
}
