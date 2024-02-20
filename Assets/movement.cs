using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    float Mspeed = 1f;

    [SerializeField]
    //float Rspeed = 1f;

    Vector2 lastMousePos = new Vector2(0,0);

    // Update is called once per frame
    void Update()
    {
        MyVector3 forwardDirection = MathLib.EulerAnglesToDirection(transform.eulerAngles / (180.0f/Mathf.PI));
        MyVector3 RightDirection = MathLib.VectorCrossProduct(Vector3.up, forwardDirection.ToUnityVector());

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += forwardDirection.ToUnityVector() * Time.deltaTime * Mspeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= forwardDirection.ToUnityVector() * Time.deltaTime * Mspeed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += RightDirection.ToUnityVector() * Time.deltaTime * Mspeed;
        }
        if(Input.GetKey(KeyCode.A)) 
        {
            transform.position -= RightDirection.ToUnityVector() * Time.deltaTime * Mspeed;
        }

        Vector2 mousePos = Input.mousePosition;
       
        Vector2 mouseDelta = new Vector2(0,0);

        mouseDelta = mousePos - lastMousePos;

        lastMousePos = mousePos;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x - mouseDelta.y, transform.eulerAngles.y + mouseDelta.x, 0);
    }


}
