using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLERPing : MonoBehaviour
{
    GameObject Planet1;
    GameObject Planet2;
    void Start()
    {
        GameObject Planet1 = GameObject.Find("Planet1");
        GameObject Planet2 = GameObject.Find("Planet2");
    }

    [SerializeField]
    float t = 0;

    [SerializeField]
    Vector3 TargetLocation = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            CameraWarp(Planet1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CameraWarp(Planet2);
        }
        t += Time.deltaTime * 0.5f;

        Quat q = new Quat(Mathf.PI * 0.5f, new MyVector3(0, 1, 0));
        Quat r = new Quat(Mathf.PI * 0.25f, new MyVector3(1, 0, 0));

        Quat slerped = Quat.SLERP(q, r, t);

        MyVector3 p = new MyVector3(1, 2, 3);

        Quat K = new Quat(p);

        Quat newK = slerped * K * slerped.Inverse();

        MyVector3 newP = newK.GetAxis();

        transform.position = newP.ToUnityVector();
    }

    void CameraWarp(GameObject Planet)
    {
        TargetLocation = Planet.transform.position;
    }
}
