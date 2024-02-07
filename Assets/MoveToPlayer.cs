using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    private float speed = 5.0f;


    void Update()
    {
        Vector3 LocationofCube = GameObject.Find("Cube").transform.position;
        MyVector3 cubeLocation = new MyVector3(LocationofCube.x, LocationofCube.y, LocationofCube.z);
        MyVector3 selfLocation = new MyVector3(transform.position.x, transform.position.y, transform.position.z);

        MyVector3 directionVector = MyVector3.SubtractVector(cubeLocation, selfLocation).NormalizeMyVector();

        Vector3 CubeRef = GameObject.Find("Cube").transform.forward;
        MyVector3 TargetDirection = new MyVector3(CubeRef.x, CubeRef.y, CubeRef.z);

        if (MyVector3.VectorDot(directionVector, TargetDirection) >= 0.5)
        {
            selfLocation = MyVector3.AddVector(directionVector * Time.deltaTime * speed, selfLocation);
            transform.position = selfLocation.ToUnityVector();
        }

        
    }
}
