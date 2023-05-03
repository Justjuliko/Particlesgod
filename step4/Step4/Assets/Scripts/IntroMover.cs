using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMover
{
    // The location of the mover
    Vector2 location;

    // The window limits
    private Vector2 maximumPos, step_one, step_two;

    // Range over which height and width varies.
    float heightScale = .7f;
    float widthScale = 1.0f;

    // Distance covered per second along X and Y axis of Perlin plane.
    float xScale = 1.0f;
    float yScale = .5f;

    float x_scale = 2.0f;
    float y_scale = 1.0f;

    // Gives the class a GameObject to draw on the screen
    public GameObject moverGO;

    public float timeSinceReset;
    public float resetTime;
    public Color blackColor;
    public float fade_speed = 0.1f;
    private float G;
    private Rigidbody body;

    public IntroMover(GameObject ghost)
    {
        moverGO = ghost;
        body = moverGO.GetComponent<Rigidbody>();
        FindWindowLimits();
        step_one = Vector2.zero;
        step_two = Vector2.zero;
        location = Vector2.zero;
        G = 9.8f;
        body.mass = 33;
        body.useGravity = false;
        body.isKinematic = true;
    }
    public Vector2 Attract(Rigidbody m)
    {
        Vector2 force = body.position - m.position;
        float distance = force.magnitude;

        // Remember we need to constrain the distance so that our circle doesn't spin out of control
        distance = Mathf.Clamp(distance, 5f, 25f);

        force.Normalize();
        float strength = (G * body.mass * m.mass) / (distance * distance);
        force *= strength;
        return force;
    }
    public void Step(GameObject ghost)
    {
        float second_step_x = widthScale * Mathf.PerlinNoise(Time.time * x_scale, 0.0f) * timeSinceReset;
        float second_step_y = heightScale * Mathf.PerlinNoise(0.0f, Time.time * y_scale) * timeSinceReset;
        step_two = new Vector2(second_step_x, second_step_y);
        float one_step_x = widthScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f) * timeSinceReset;
        float one_step_y = heightScale * Mathf.PerlinNoise(0.0f, Time.time * yScale) * timeSinceReset;
        step_one = new Vector2(one_step_x, one_step_y);
        Vector3 pos = moverGO.transform.position;
        float steps = MonteCarlo();
        if (steps == 1)
        {
            pos = step_two;
        }
        else
        {
            pos = step_one;
        }
        moverGO.transform.position = pos;
    }
    float MonteCarlo()
    {
        while (true)
        {
            float r1 = Random.value;
            float probability = r1;
            float r2 = Random.value;

            if (r2 < probability)
            {
                return r1;
            }
        }
    }
    public void CheckEdges()
    {
        location = moverGO.transform.position;
        if (location.x > maximumPos.x || location.x < -maximumPos.x)
        {
            Reset();
        }

        if (location.y > maximumPos.y || location.y < -maximumPos.y)
        {
            Reset();
        }
        moverGO.transform.position = location;
    }

    void Reset()
    {
        location = Vector2.zero;
        resetTime = Time.time;
        heightScale = Random.Range(-1f, 1f);
        widthScale = Random.Range(-1f, 1f);
    }

    private void FindWindowLimits()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the maximum position for the screen
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

   
            
}
    
