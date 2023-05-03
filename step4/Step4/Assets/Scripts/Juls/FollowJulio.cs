using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowJulio : MonoBehaviour
{
    Rigidbody Rigidbodyrb;
    public GameObject followthis;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbodyrb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbodyrb.AddForce(followthis.transform.position - transform.position);
    }
}
