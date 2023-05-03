using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float attractionForce = 5.0f;
    public float repulsionForce = 2.0f;
    public float attractionRadius = 5.0f;
    public float repulsionRadius = 1.0f;

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = transform.position - collider.transform.position;
                float distance = direction.magnitude;

                if (distance > attractionRadius)
                {
                    // Atrae el objeto
                    rb.AddForce(direction.normalized * attractionForce * Time.fixedDeltaTime);
                }
                else if (distance < repulsionRadius)
                {
                    // Repule el objeto
                    rb.AddForce(-direction.normalized * repulsionForce * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja la esfera de atractión y repulsión en el editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, repulsionRadius);
    }
}
