using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB
{
    Vector3 MinExtent;
    Vector3 MaxExtent;
   public AABB(Vector3 min, Vector3 max)
    {
        MinExtent = min;
        MaxExtent = max;
    }

    public static bool Intersects(AABB Box1, AABB Box2)
    {
        return !(Box2.Left > Box1.Right
            || Box2.Right < Box1.Left
            || Box2.Top < Box1.Bottom
            || Box2.Bottom > Box1.Top
            || Box2.Back > Box1.Front
            || Box2.Front < Box1.Back); 
    }

    public float Top
    {
        get { return MaxExtent.y; }
    }

    public float Bottom
    {
        get { return MinExtent.y; }
    }

    public float Left
    {
        get { return MinExtent.x; }
    }

    public float Right
    {
        get { return MaxExtent.x; }
    }

    public float Front
    {
        get { return MaxExtent.z; }
    }

    public float Back
    {
        get { return MinExtent.z; }
    }
}
