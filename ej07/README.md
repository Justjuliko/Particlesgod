# DOCUMENTACIÓN

## RESULTADO DE APRENDIZAJE 1

### ¿Qué conceptos, algoritmos, estructuras de datos y/o patrones usaste para resolver el problema?
Uso de sistema de particulas, POO, Vectores, SOLID Principles.
```csharp
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
```
### ¿Qué función cumplen los conceptos anteriores en la solución?
En el comportamiento de las particulas en el entorno y el paso de variables como texturas mediante otro codigo principal.
## RESULTADO DE APRENDIZAJE 2
### ¿Cómo probaste las partes que componen la solución?
Observando el comportamiento sin las capas y luego con las capas.
### ¿Cómo probaste la solución completa?
Comprobando que el comportamiento del ruido Perlin era diferente con las capas y sus diferentes valores.
## RESULTADO DE APRENDIZAJE 3
### ¿Qué conceptos de matemática y/o física usaste para resolver el problema?
Ruido Perlin y operaciones basicas.
### ¿En que parte de la solución y para qué estás usando el concepto?
En el comportamiento de las particulas que actuan en el entorno.
## RESULTADO DE APRENDIZAJE 4

### Agrega la URL de un video público o no listado en youtube donde muestres la solución
https://youtu.be/7ZEFn4jrKZs
