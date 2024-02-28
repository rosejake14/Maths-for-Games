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

    public static bool LineInetersection(AABB Box, Vector3 StartPoint, Vector3 EndPoint, out Vector3 IntersectionPoint)
    {
        float Lowest = 0.0f;
        float Highest = 1.0f;

        IntersectionPoint = Vector3.zero;

        if (!IntersectingAxis(Vector3.right, Box, StartPoint, EndPoint, ref Lowest, ref Highest))
        {
            return false;
        }
        if (!IntersectingAxis(Vector3.up, Box, StartPoint, EndPoint, ref Lowest, ref Highest))
        {
            return false;
        }
        if (!IntersectingAxis(Vector3.forward, Box, StartPoint, EndPoint, ref Lowest, ref Highest))
        {
            return false;
        }

        IntersectionPoint = MathLib.LinInterpolate(StartPoint, EndPoint, Lowest);

        return true;
    }

    public static bool IntersectingAxis(Vector3 Axis, AABB Box, Vector3 StartingPoint, Vector3 EndPoint, ref float Lowest, ref float Highest)
    {
        float Minimum = 0.0f, Maximum = 1.0f;
        if (Axis == Vector3.right)
        {
            Minimum = (Box.Left - StartingPoint.x) / (EndPoint.x - StartingPoint.x);
            Maximum = (Box.Right - StartingPoint.x) / (EndPoint.x - StartingPoint.x);
        }
        else if (Axis == Vector3.up)
        {
            Minimum = (Box.Bottom - StartingPoint.y) / (EndPoint.y - StartingPoint.y);
            Maximum = (Box.Top - StartingPoint.y) / (EndPoint.y - StartingPoint.y);
        }
        else if (Axis == Vector3.forward)
        {
            Minimum = (Box.Back - StartingPoint.z) / (EndPoint.z - StartingPoint.z);
            Maximum = (Box.Front - StartingPoint.z) / (EndPoint.z - StartingPoint.z);
        }

        if (Maximum < Minimum)
        {
            float temp = Maximum;
            Maximum = Minimum;
            Minimum = temp;
        }

        if (Maximum < Lowest || Minimum > Highest)
            return false;

        Lowest = Mathf.Max(Minimum, Lowest);
        Highest = Mathf.Max(Maximum, Highest);

        if(Lowest > Highest)
            return false;

        return true;
    }
}
