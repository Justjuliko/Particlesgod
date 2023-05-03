using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInitialate : MonoBehaviour
{
    Particle psf3;
    Vector3 particleSystemLocation;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float lifeTime;
    public float startSpeed;
    [SerializeField] private Material material;
    [SerializeField] private Material trailMaterial;
    int maxParticles;

    // Start is called before the first frame update
    void Start()
    {
        //Let's just have one particle
        maxParticles = 1000;
        psf3 = new Particle(particleSystemLocation, startSpeed, velocity, lifeTime, maxParticles, material, trailMaterial);
    }
}
