using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolisticMath
{
    static public Coords GetNormal(Coords vector)
    {
        float mag = Distance(new Coords(Vector3.zero), vector);
        return (new Coords(vector.x / mag, vector.y / mag, vector.z / mag));
    }

    static public float Distance(Coords point1, Coords point2)
    {
        Coords distanceCoord = new Coords(point2.x - point1.x, point2.y - point1.y, point2.z - point1.z);
        return (Mathf.Sqrt(Square(distanceCoord.x) + Square(distanceCoord.y + Square(distanceCoord.z))));
    }

    static public float Square(float value)
    {
        return (value * value);
    }

    static public float Dot(Coords vector1, Coords vector2)
    {
        return ((vector1.x * vector2.x) + (vector1.y * vector2.y) + (vector1.z * vector2.z));
    }

    static public float Angle(Coords vector1, Coords vector2)
    {
        float vector1Dist = Distance(new Coords(Vector3.zero), vector1);
        float vector2Dist = Distance(new Coords(Vector3.zero), vector2);
        return (Mathf.Acos(Dot(vector1, vector2) / (vector1Dist * vector2Dist))); //radians. need to multiply by 180/pi for degrees
    }

    static public Coords Rotate(Coords vector, float angle, bool clockwise) // in radians
    {
        if (clockwise)
            angle = 2 * Mathf.PI - angle;

        float xVal = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float yVal = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);
        return new Coords(xVal, yVal, 0.0f);
    }

    static public Coords Cross(Coords vector1, Coords vector2)
    {
        float crossX = vector1.y * vector2.z - vector1.z * vector2.y;
        float crossY = vector1.z * vector2.x - vector1.x * vector2.z;
        float crossZ = vector1.x * vector2.y - vector1.y * vector2.x;
        return new Coords(crossX, crossY, crossZ);
    }

    static public Coords LookAt2D(Vector3 forwardVec, Coords position, Coords target)
    {
        Coords direction = target - position;
        Coords dirNormal = HolisticMath.GetNormal(direction);
        float angle = HolisticMath.Angle(new Coords(forwardVec), direction);
        bool clockwise = false;
        if (HolisticMath.Cross(new Coords(forwardVec), dirNormal).z < 0)
            clockwise = true;

        Coords newDir = HolisticMath.Rotate(new Coords(forwardVec), angle, clockwise);
        return newDir;
    }
}
