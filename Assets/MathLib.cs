using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLib
{
   
    public static float VectorToRadians(Vector3 v)
    {
        float rv = .0f;

        rv = Mathf.Atan(v.y / v.x);

        return rv;
    }

    public static Vector2 RadiansToVector(float angle)
    {
        Vector2 rv = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        return rv;
    }

    public static MyVector3 VectorCrossProduct(MyVector3 v1, MyVector3 v2)
    {
        MyVector3 cp = new MyVector3(0, 0, 0);

        cp.x = v1.x * v2.z - v1.z * v2.y;
        cp.y = v1.z * v2.x - v1.x * v2.z;
        cp.z = v1.x * v2.x - v1.x * v2.z;

        return cp;
    }

}
