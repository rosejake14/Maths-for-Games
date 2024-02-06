using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector3
{
    public float x, y, z;

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public MyVector3 AddVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x + b.x; 
        rv.y = a.y + b.y;
        rv.z = a.z + b.z;

        return rv;
    }

    public static MyVector3 SubtractVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x - b.x;
        rv.y = a.y - b.y;
        rv.z = a.z - b.z;

        return rv;
    }

    public float Length()
    {
        float rv = .0f;
        rv = Mathf.Sqrt(x * x + y * y + z * z);
        return rv;
    }

    public Vector3 ToUnityVector()
    {
        Vector3 rv = new Vector3(x,y,z);

        return rv;
    }

    public float LengthSquared()
    {
        float rv = .0f;

        rv = x * x + y * y + z * z;

        return rv;
    }

    public MyVector3 Scale(MyVector3 vector, float scale)
    {
        MyVector3 rv = new MyVector3(0,0,0);

        rv.x = vector.x * scale;
        rv.y = vector.y * scale;
        rv.z = vector.z * scale;
       
        return rv;
    }

    public MyVector3 Divide(MyVector3 vector, float divisor)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = vector.x / divisor;
        rv.y = vector.y / divisor;
        rv.z = vector.z / divisor;

        return rv;
    }

    public MyVector3 NormalizeMyVector()
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv = Divide(this, Length());

        return rv;
    }


    static float VectorDot(MyVector3 v1, MyVector3 v2, bool shouldNormalize = true)
    {
        float rv = .0f;

        if(shouldNormalize)
        {
            MyVector3 v1normal = v1.NormalizeMyVector();
            MyVector3 v2normal = v2.NormalizeMyVector();

            rv = v1normal.x * v2normal.x + v1normal.y * v2normal.y + v1normal.z * v2normal.z;
        }
        else
        {
            rv = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        return rv;
    }

}
