using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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

    public static Vector3 LinInterpolate(Vector3 v1, Vector3 v2, float t)
    {
        Vector3 rv = new Vector3(0, 0, 0);

        rv = v1 * (1.0f - t) + v2 * t;

        return rv;
    }

    public MyVector3 RotateVertexAroundAxis(float Angle, MyVector3 Axis, MyVector3 Vertex)
    {
        MyVector3 rv = Vertex * Mathf.Cos(Angle) +
            Axis * MyVector3.VectorDot(Vertex, Axis) * (1.0f - Mathf.Cos(Angle)) +
            VectorCrossProduct(Axis.ToUnityVector(), Vertex.ToUnityVector()) * Mathf.Sin(Angle);

        return rv;
    }
}

public class Quat
{
    public float w;
    public MyVector3 v;

    public Quat()
    {
        w = 0.0f;
        v = new MyVector3(0, 0, 0);
    }
    public Quat(float Angle, MyVector3 Axis)
    {
        float halfAngle = Angle / 2;

        w = Mathf.Cos(halfAngle);
        v = Axis * Mathf.Sin(halfAngle);
    }

    public Quat(MyVector3 Position)
    {
        w = 0.0f;
        v = new MyVector3(Position.x, Position.y, Position.z);
    }

    public static Quat operator*(Quat lhs, Quat rhs)
    {
        Quat rv = new Quat();

        rv.w = (lhs.w * rhs.w) - MyVector3.VectorDot(lhs.v, rhs.v, false);
        rv.v = (rhs.v * lhs.w) + (lhs.v * rhs.w) + (MathLib.VectorCrossProduct(rhs.v.ToUnityVector(), lhs.v.ToUnityVector()));

        return rv;

    }

    public void SetAxis(MyVector3 Axis)
    {
        v = Axis;
    }

    public MyVector3 GetAxis()
    {
        return v;
    }
    public Quat Inverse()
    {
        Quat rv = new Quat();

        rv.w = w;

        rv.SetAxis(-GetAxis());

        return rv;
    }

    public Vector4 GetAxisAngle()
    {
        Vector4 rv = new Vector4();

        float halfAngle = Mathf.Acos(w);
        rv.w = halfAngle * 2;

        rv.x = v.x / Mathf.Sin(halfAngle);
        rv.y = v.y / Mathf.Sin(halfAngle);
        rv.z = v.z / Mathf.Sin(halfAngle);

        return rv;
    }

    public static Quat SLERP(Quat q, Quat r, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);

        Quat d = r * q.Inverse();
        Vector4 AxisAngle = d.GetAxisAngle();
        Quat dT = new Quat(AxisAngle.w * t, new MyVector3(AxisAngle.x, AxisAngle.y, AxisAngle.z));

        return dT * q;
    }

    public Matrix4by4 QuatToMatrix()
    {
        Matrix4by4 rv = Matrix4by4.Identity;

        float xx = v.x * v.x;
        float xy = v.x * v.y;
        float xz = v.x * v.z;
        float xw = v.x * w;

        float yy = v.y * v.y;
        float yz = v.y * v.z;
        float yw = v.y * w;

        float zz = v.z * v.z;
        float zw = v.z * w;


        rv.values[0, 0] = 1 - 2*(yy + zz);
        rv.values[1, 0] = 2*(xy - zw);
        rv.values[2, 0] = 2*(xz + yw);
        rv.values[3, 0] = 0;

        rv.values[0, 1] = 2*(xy + zw);
        rv.values[1, 1] = 1 - 2 * (xx + zz);
        rv.values[2, 1] = 2 * (yz - xw);
        rv.values[3, 1] = 0;

        rv.values[0, 2] = 2 * (xz - yw);
        rv.values[1, 2] = 2*(yz + xw);
        rv.values[2, 2] = 1 - 2*(xx + yy);
        rv.values[3, 2] = 0;

        rv.values[0, 3] = 0;
        rv.values[1, 3] = 0;
        rv.values[2, 3] = 0;
        rv.values[3, 3] = 1;

        return rv.RotationInverse();
    }
}


