using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step4 : MonoBehaviour
{
    [SerializeField] float period = 5;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= 0f)
        {
            i = i + 1;
        }
        float cycles = Time.time / period;

        if(i == 100)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.localScale= new Vector3(1, 1, 1);
            float health = i / 100;
            Debug.Log("La vida de la comida uno es:");
            Debug.Log(health);
            transform.GetChild(4).gameObject.SetActive(true);
        }
        if (i == 400)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).transform.localScale = new Vector3(2, 2, 2);
            float health = i / 20;
            Debug.Log("La vida de la comida dos es:");
            Debug.Log(health);
            transform.GetChild(5).gameObject.SetActive(true);
        }
        if (i == 700)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).transform.localScale = new Vector3(3, 3, 3);
            float health = i / 35;
            Debug.Log("La vida de la comida tres es:");
            Debug.Log(health);
            transform.GetChild(6).gameObject.SetActive(true);
        }
        if (i == 1000)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(3).transform.localScale = new Vector3(4, 4, 4);
            float health = i ;
            Debug.Log("La vida de la comida cuatro es:");
            Debug.Log(health);
            transform.GetChild(7).gameObject.SetActive(true);
        }

    }
}
