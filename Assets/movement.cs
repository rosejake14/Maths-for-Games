using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    float Mspeed = 1f;

    [SerializeField]
    float Rspeed = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * Mspeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * Mspeed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + (Time.deltaTime * Rspeed), 0);
        }
        if(Input.GetKey(KeyCode.A)) 
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - (Time.deltaTime * Rspeed), 0);
        }
    }
}
