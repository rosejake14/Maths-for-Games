using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float speed = 0.1f;
    // Update is called once per frame
    void Update(float DeltaTime)
    {
        MyVector3 cubeLocation = new MyVector3(GameObject.Find("Cube").transform.position.x, GameObject.Find("Cube").transform.position.y, GameObject.Find("Cube").transform.position.z);
        MyVector3 selfLocation = new MyVector3(transform.position.x, transform.position.y, transform.position.z);

        selfLocation = MyVector3.SubtractVector(cubeLocation, selfLocation).NormalizeMyVector();

        transform.position = selfLocation.ToUnityVector() * DeltaTime * speed;
    }
}
