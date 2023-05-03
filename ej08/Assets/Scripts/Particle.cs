using System.Collections;
using UnityEngine;

public class Particle
{
    //We need to create a GameObject to hold the ParticleSystem component
    GameObject particleSystemGameObject;

    //This is the ParticleSystem component but we'll need to access everything through the .main property
    //This is because ParticleSystems in Unity are interfaces and not independent objects
    ParticleSystem particleSystemComponent;

    public Particle(Vector3 particleSystemLocation, float startSpeed, Vector3 velocity, float lifeTime, int maxParticles, Material material, Material trailMaterial)
    {
        //Create the GameObject in the constructor
        particleSystemGameObject = new GameObject();
        //Move the GameObject to the right position
        particleSystemGameObject.transform.position = particleSystemLocation;
        //Add the particle system
        particleSystemComponent = particleSystemGameObject.AddComponent<ParticleSystem>();

        //Now we need to gather the interfaces of our ParticleSystem
        //The main interface covers general properties
        var main = particleSystemComponent.main;

        //In the Main Interface we'll sat the initial start LifeTime (how long a single particle will live)
        //And, of course, we'll set our Max Particles
        main.startLifetime = lifeTime;
        main.startSpeed = startSpeed;
        main.maxParticles = maxParticles;
        main.gravityModifier = 1;

        //Now we'll call methods to control the velocity of individual particles and their colors
        velocityModule(velocity);
        colorModule(material);
        //Now let's add some trails
        trailModule(trailMaterial);
        noiseModule();
        rotationBySpeed(velocity);
        rotationOverLifetime(velocity);
    }
    public void noiseModule()
    {
        //The Noise Module manages the distortion and randomness of the particles
        var noiseModule = particleSystemComponent.noise;
        //To have the particle become transparent we need to access the colorOverLifetime Interface
        noiseModule.enabled = true;
        //The strength of the turbulence
        noiseModule.strength = 1.0f;
        //High quality settings simulate smoother transitions
        noiseModule.quality = ParticleSystemNoiseQuality.High;
        //Next we can have the noise scroll which adds even more randomness. We can set it to a constant or curve.
        noiseModule.scrollSpeed = 4;
        //We can even add some noise to the scale of each particle
        noiseModule.sizeAmount = 3;
        noiseModule.octaveCount = 3;
        noiseModule.octaveScale = 0.1f;
        noiseModule.octaveMultiplier = 0.1f;
    }
    public void velocityModule(Vector3 velocity)
    {
        //The velocityOverLifetime inferface controls the velocity of individual particles
        var velocityOverLifetime = particleSystemComponent.velocityOverLifetime;

        //First we need to enable the Velocity Over Lifetime Interface;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.Local;

        //We then to create a MinMaxCurves which will manage the change in velocity a
        ParticleSystem.MinMaxCurve minMaxCurveX = new ParticleSystem.MinMaxCurve(-velocity.x * velocity.x, velocity.x);
        ParticleSystem.MinMaxCurve minMaxCurveY = new ParticleSystem.MinMaxCurve(velocity.y, -velocity.y);

        velocityOverLifetime.x = minMaxCurveX;
        velocityOverLifetime.y = minMaxCurveY;
        //Even though we are not using Z, Unity needs us to otherwise it will throw an error. 
        //This is a bug in 2019.
        velocityOverLifetime.z = minMaxCurveY;
    }

    public void colorModule(Material material)
    {
        //The colorOverLifetime interfaces manages the color of the objects over their lifetime.
        var colorOverLifetime = particleSystemComponent.colorOverLifetime;
        colorOverLifetime.enabled = true;


        //While we are here, let's add a material to our particles
        ParticleSystemRenderer r = particleSystemGameObject.GetComponent<ParticleSystemRenderer>();
        //There a few different ways to do this, but we've created a material that is based on the default particle shader
        r.material = material;

        Gradient grad = new Gradient();
        //This gradient key lets us choose points on a gradient that represent different RGBA or Unity.Color values.
        //These gradient values exist in an array
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0f, 2.0f) });
        //Set the color to the gradient we created above
        colorOverLifetime.color = grad;
    }

    public void trailModule(Material trailMaterial)
    {
        //The Trails Modules interface manages the color of individual particle trails
        var trailsModule = particleSystemComponent.trails;
        trailsModule.enabled = true;
        //This is how many particles will receive the trails. Setting it to 1 means they all will. 
        trailsModule.ratio = .5f;
        //Next we want the trail to die when the particle dies 
        trailsModule.dieWithParticles = true;
        //And we want these trails to act like a ribbon
        trailsModule.mode = ParticleSystemTrailMode.Ribbon;
        //We also want a few of these ribbons
        trailsModule.ribbonCount = 10;

        //Lastly, we want the trails to all connect to an origin point to create a many-legs effect
        trailsModule.attachRibbonsToTransform = true;

        //Let's add some color by having the trail shift between two different color values
        Gradient grad = new Gradient();
        //This gradient key lets us choose points on a gradient that represent different RGBA or Unity.Color values.
        //These gradient values exist in an array
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1f, 2.0f) });
        //Set the color to the gradient we created above
        trailsModule.colorOverLifetime = grad;

        //While we are here, let's add a material to our particle trails
        ParticleSystemRenderer r = particleSystemGameObject.GetComponent<ParticleSystemRenderer>();
        //There a few different ways to do this, but we've created a material that is based on the default particle shader
        r.trailMaterial = trailMaterial;
    }
    public void rotationOverLifetime(Vector3 rotationVelocity)
    {
        var rotationOverLifetime = particleSystemComponent.rotationOverLifetime;
        rotationOverLifetime.enabled = true;
        //we'll now go ahead and rotate 360-degrees on the x and y axes.
        rotationOverLifetime.separateAxes = true;
        //Now let's pass on the floats in our Vector3
        rotationOverLifetime.x = rotationVelocity.x;
        rotationOverLifetime.y = rotationVelocity.y;
        rotationOverLifetime.z = rotationVelocity.z;
    }
    public void rotationBySpeed(Vector3 rotationVelocity)
    {
        var rotationBySpeed = particleSystemComponent.rotationBySpeed;
        rotationBySpeed.enabled = true;
        //we'll now go ahead and rotate 360-degrees on the x and y axes.
        rotationBySpeed.separateAxes = true;
        //Now let's pass on the floats in our Vector3
        //This time let's pass the Y into the Z
        rotationBySpeed.x = 0f;
        rotationBySpeed.y = 0f;
        rotationBySpeed.z = rotationVelocity.y;
    }

    public IEnumerator ModifyGravityOverTime(float duration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float gravity = Mathf.Sin(timeElapsed * 2f * Mathf.PI / duration) * 10f;
            var main = particleSystemComponent.main;
            main.gravityModifierMultiplier = gravity;
            yield return new WaitForSeconds(0.1f);
            timeElapsed += 0.1f;
        }
    }

}
