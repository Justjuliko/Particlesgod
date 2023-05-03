using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ghost;
    IntroMover mover;
    List<Fishis> fishis = new List<Fishis>();
    void Start()
    {
        mover = new IntroMover(ghost);
        int numberOfFishis = 10;
        for (int i = 0; i < numberOfFishis; i++)
        {
            Vector2 randomLocation = new Vector2(Random.Range(-7f, 7f), Random.Range(-7f, 7f));
            Vector2 randomVelocity = new Vector2(Random.Range(0f, 5f), Random.Range(0f, 5f));
            Fishis m = new Fishis(Random.Range(0.2f, 1f), randomVelocity, randomLocation); //Each Mover is initialized randomly.
            fishis.Add(m);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mover.timeSinceReset = Time.time - mover.resetTime;
       
        mover.Step(ghost);
        
        mover.CheckEdges();
        
    }
    private void FixedUpdate()
    {
        foreach (Fishis m in fishis)
        {
            Rigidbody body = m.body;
            Vector2 force = mover.Attract(body); // Apply the attraction from the Attractor on each Mover object

            m.ApplyForce(force);
            m.CalculatePosition();
        }
    }

}
