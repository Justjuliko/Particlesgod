using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juancho2 : MonoBehaviour

{ // Geometry defined in the inspector.
    [SerializeField] float floorY;
    [SerializeField] float leftWallX;
    [SerializeField] float rightWallX;
    [SerializeField] Transform moverSpawnTransform;


    // Create a list of movers
    private List<Mover1_20> movers = new List<Mover1_20>();

    // Define constant forces in our environment
    private Vector3 wind = new Vector3(0.002f, 0f, 0f);
    private float frictionStrength = 200f;


    // Start is called before the first frame update
    void Start()
    {
        // Create copies of our mover and add them to our list
        while (movers.Count < 10)
        {
            movers.Add(new Mover1_20(moverSpawnTransform.position, leftWallX, rightWallX, floorY));
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        foreach (Mover1_20 mover in movers)
        {
            // ForceMode.Impulse takes mass into account
            mover.body.AddForce(wind, ForceMode.Impulse);

            // Apply a friction force that directly opposes the current motion
            Vector3 friction = -mover.body.velocity;
            friction.Normalize();
            friction *= frictionStrength;
            mover.body.AddForce(friction, ForceMode.Force);

            mover.CheckEdges();
        }
    }
}

public class Mover1_20
{
    public Rigidbody body;
    private GameObject gameObject;
    private float radius;

    private float xMin = -5;
    private float xMax = 5;
    private float yMin;
    private float xSpawn;

    public Mover1_20(Vector3 position, float xMin, float xMax, float yMin)
    {
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;

        // Create the components required for the mover
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body = gameObject.AddComponent<Rigidbody>();

      

        // Generate random properties for this mover
        radius = Random.Range(0.1f, 0.4f);

        // Generate a random x value within the bundaries
        xSpawn = Random.Range(xMin, xMax);

        // Place our mover at a randomized spawn position relative
        // to the bottom of the sphere
        gameObject.transform.position = new Vector3(xSpawn, position.y = 0, position.z = 0) + Vector3.up * radius;

        // The default diameter of the sphere is one unit
        // This means we have to multiple the radius by two when scaling it up
        gameObject.transform.localScale = 2 * radius * Vector3.one;

        // We need to calculate the mass of the sphere.
        // Assuming the sphere is of even density throughout,
        // the mass will be proportional to the volume.
        body.mass = (4f / 3f) * Mathf.PI * radius * radius * radius;
    }

    // Checks to ensure the body stays within the boundaries
    public void CheckEdges()
    {
        Vector3 restrainedVelocity = body.velocity;
        if (body.position.y - radius < yMin)
        {
            // Using the absolute value here is an important safe
            // guard for the scenario that it takes multiple ticks
            // of FixedUpdate for the mover to return to its boundaries.
            // The intuitive solution of flipping the velocity may result
            // in the mover not returning to the boundaries and flipping
            // direction on every tick.

            restrainedVelocity.y = Mathf.Abs(restrainedVelocity.y);
            body.position = new Vector3(body.position.x, yMin, body.position.z) + Vector3.up * radius;
        }
        if (body.position.x - radius < xMin)
        {
            restrainedVelocity.x = Mathf.Abs(restrainedVelocity.x);
            body.position = new Vector3(xMin, body.position.y, body.position.z) + Vector3.right * radius;
        }
        else if (body.position.x + radius > xMax)
        {
            restrainedVelocity.x = -Mathf.Abs(restrainedVelocity.x);
            body.position = new Vector3(xMax, body.position.y, body.position.z) + Vector3.left * radius;
        }
        body.velocity = restrainedVelocity;
    }
}