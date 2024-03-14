using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuatMovement : MonoBehaviour
{

    [SerializeField]
    Vector3 OriginRotationPoint = new Vector3();

    [SerializeField]
    public float MovementMultiplier = 1.0f;

    float angle = 0;

    void Update()
    {
        //Quat Movement Rotations
            angle += Time.deltaTime * MovementMultiplier;

            Quat q = new Quat(angle, new MyVector3(0, 1, 0));

            MyVector3 p = new MyVector3(OriginRotationPoint.x, OriginRotationPoint.y, OriginRotationPoint.z);

            Quat K = new Quat(p);

            Quat newK = q * K * q.Inverse();

            MyVector3 newP = newK.GetAxis();

            transform.position = newP.ToUnityVector();

        
    }

}
