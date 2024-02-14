using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLib
{
    public static float VectorToRadians(Vector2 v)
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

    public static MyVector3 EulerAnglesToDirection(Vector3 EulerAngles)
    {
        MyVector3 rv = new MyVector3(0,0,0);

        rv.z = Mathf.Cos(EulerAngles.y) * Mathf.Cos(-EulerAngles.x);
        rv.y = Mathf.Sin(-EulerAngles.x);
        rv.x = Mathf.Cos(-EulerAngles.x) * Mathf.Sin(EulerAngles.y);

        return rv;
    }

    public static MyVector3 VectorCrossProduct(Vector3 v1, Vector3 v2)
    {
        MyVector3 cp = new MyVector3(0, 0, 0);

        cp.x = v1.y * v2.z - v1.z * v2.y;
        cp.y = v1.z * v2.x - v1.x * v2.z;
        cp.z = v1.x * v2.y - v1.y * v2.x;

        return cp;
    }

}
