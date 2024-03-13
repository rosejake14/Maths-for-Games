using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SLERPing : MonoBehaviour
{
    
    void Start()
    {
        
    }

    float t = 0;
   
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * 0.5f;


        Quat q = new Quat(Mathf.PI * .5f, new MyVector3(0, 1, 0));
        Quat r = new Quat(Mathf.PI * .25f, new MyVector3(1, 0, 0));

        Quat slerped = Quat.SLERP(q, r, t);

        MyVector3 p = new MyVector3(1, 2, 3);

        Quat K = new Quat(p);

        Quat newK = slerped * K * slerped.Inverse();

        MyVector3 newP = newK.GetAxis();

        transform.position = newP.ToUnityVector();
    }

}
