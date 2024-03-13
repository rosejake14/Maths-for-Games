using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuatMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float angle = 0;

    [SerializeField]
    Vector3 OriginRotationPoint = new Vector3();

    [SerializeField]
    float MovementMultiplier = 1.0f;

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * MovementMultiplier;

        Quat q = new Quat(angle, new MyVector3(0, 1, 0));

        MyVector3 p = new MyVector3(OriginRotationPoint.x, OriginRotationPoint.y, OriginRotationPoint.z);

        Quat K = new Quat(p);

        Quat newK = q * K * q.Inverse();

        MyVector3 newP = newK.GetAxis();

        transform.position = newP.ToUnityVector();
    }
}
