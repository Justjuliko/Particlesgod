# DOCUMENTACIÓN

## RESULTADO DE APRENDIZAJE 1

### ¿Qué conceptos, algoritmos, estructuras de datos y/o patrones usaste para resolver el problema?
Corutinas, POO.

### ¿Qué función cumplen los conceptos anteriores en la solución?
En el comportamiento y la aplicacion de la gravedad a las particulas.
```csharp
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
```
## RESULTADO DE APRENDIZAJE 2

### ¿Cómo probaste las partes que componen la solución?
Observando el comportamiento de las particulas.
### ¿Cómo probaste la solución completa?
Cambiando los valores de "duration" para observar como cambiaba la gravedad de las particulas con el tiempo.
## RESULTADO DE APRENDIZAJE 3

### ¿Qué conceptos de matemática y/o física usaste para resolver el problema?
Funciones seno y vectores.
### ¿En que parte de la solución y para qué estás usando el concepto?
En el calculo de la gravedad para aplicarlo a las particulas.
## RESULTADO DE APRENDIZAJE 4
https://youtu.be/E2s76J5bsdk
### Agrega la URL de un video público o no listado en youtube donde muestres la solución
