using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolisticMath
{
    static public Coords GetNormal(Coords vector)
    {
        float length = Distance(new Coords(0, 0, 0), vector);
        vector.x /= length;
        vector.y /= length;
        vector.z /= length;

        return vector;
    }

    static public float Distance(Coords point1, Coords point2)
    {
        float diffSquared = Square(point1.x - point2.x) + 
                            Square(point1.y - point2.y) + 
                            Square(point1.z - point2.z);
        float squareRoot = Mathf.Sqrt(diffSquared);
        return squareRoot;

    }

    static public Coords Lerp(Coords A, Coords B, float t)
    {
        t = Mathf.Clamp(t, 0, 1);
        Coords v = new Coords(B.x - A.x, B.y - A.y, B.z - A.z);
        float xt = A.x + v.x * t;
        float yt = A.y + v.y * t;
        float zt = A.z + v.z * t;

        return new Coords(xt, yt, zt);
    }

    static public float Square(float value)
    {
        return value * value;
    }

    static public float Dot(Coords vector1, Coords vector2)
    {
        return (vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z);
    }

    static public float Angle(Coords vector1, Coords vector2)
    {
        float dotDivide = Dot(vector1, vector2) /
                    (Distance(new Coords(0, 0, 0), vector1) * Distance(new Coords(0, 0, 0), vector2));
        return Mathf.Acos(dotDivide); //radians.  For degrees * 180/Mathf.PI;
    }

    static public Coords LookAt2D(Coords forwardVector, Coords position, Coords focusPoint)
    {
        Coords direction = new Coords(focusPoint.x - position.x, focusPoint.y - position.y, position.z);
        float angle = HolisticMath.Angle(forwardVector, direction);
        bool clockwise = false;
        if (HolisticMath.Cross(forwardVector, direction).z < 0)
            clockwise = true;

        Coords newDir = HolisticMath.Rotate(forwardVector, angle, clockwise);
        return newDir;
    }

    static public Coords Rotate(Coords vector, float angle, bool clockwise) //in radians
    {
        if(clockwise)
        {
            angle = 2 * Mathf.PI - angle;
        }

        float xVal = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float yVal = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);
        return new Coords(xVal, yVal, 0);
    }
   
    static public Coords Translate(Coords position, Coords facing, Coords vector)
    {
        if (HolisticMath.Distance(new Coords(0, 0, 0), vector) <= 0) return position;
        float angle = HolisticMath.Angle(vector, facing);
        float worldAngle = HolisticMath.Angle(vector, new Coords(0, 1, 0));
        bool clockwise = false;
        if (HolisticMath.Cross(vector, facing).z < 0)
            clockwise = true;

        vector = HolisticMath.Rotate(vector, angle + worldAngle, clockwise);

        float xVal = position.x + vector.x;
        float yVal = position.y + vector.y;
        float zVal = position.z + vector.z;
        return new Coords(xVal, yVal, zVal);
    }

    static public Coords Translate(Coords position, Coords vector)
    {
        Matrix pMatrix = new Matrix(4, 1, position.AsFloats());
        Matrix tMatrix = Matrix.GetTranslationMatrix(vector);
        return (tMatrix * pMatrix).AsCoords();
    }

    static public Coords Scale(Coords position, float scaleX, float scaleY, float scaleZ)
    {
        float[] sValues = {scaleX, 0, 0, 0,
                            0, scaleY, 0, 0,
                            0, 0, scaleZ, 0,
                            0, 0, 0, 1};
        Matrix sMatrix = new Matrix(4, 4, sValues);
        Matrix pMatrix = new Matrix(4, 1, position.AsFloats());
        return (sMatrix * pMatrix).AsCoords();
    }

    static public Coords Rotate(Coords position, float angleX, bool clockwiseX,
                                                    float angleY, bool clockwiseY,
                                                    float angleZ, bool clockwiseZ)
    {
        if (clockwiseX)
        {
            angleX = 2 * Mathf.PI - angleX;
        }
        if (clockwiseY)
        {
            angleY = 2 * Mathf.PI - angleY;
        }
        if (clockwiseZ)
        {
            angleZ = 2 * Mathf.PI - angleZ;
        }

        float[] xrollValues = {1,0,0,0,
                               0,Mathf.Cos(angleX),-Mathf.Sin(angleX),0,
                               0,Mathf.Sin(angleX), Mathf.Cos(angleX),0,
                               0,0,0,1};
        Matrix XRoll = new Matrix(4, 4, xrollValues);

        float[] yrollValues = {Mathf.Cos(angleY),0,Mathf.Sin(angleY),0,
                               0,1,0,0,
                               -Mathf.Sin(angleY),0,Mathf.Cos(angleY),0,
                               0,0,0,1};
        Matrix YRoll = new Matrix(4, 4, yrollValues);

        float[] zrollValues = {Mathf.Cos(angleZ),-Mathf.Sin(angleZ),0,0,
                               Mathf.Sin(angleZ),Mathf.Cos(angleZ),0,0,
                               0,0,1,0,
                               0,0,0,1};
        Matrix ZRoll = new Matrix(4, 4, zrollValues);

        Matrix pos = new Matrix(4, 1, position.AsFloats());

        Matrix result = ZRoll * YRoll * XRoll * pos;

        return result.AsCoords();
    }

    static public Coords QRotate(Coords position, Coords axis, float angle) //in degrees
    {
        float w = Mathf.Cos(angle * Mathf.Deg2Rad / 2);
        float s = Mathf.Sin(angle * Mathf.Deg2Rad / 2);
        Coords axisNormal = axis.GetNormal();
        Coords q = new Coords(axisNormal.x * s, axisNormal.y * s, axisNormal.z * s, w);

        float[] qRotationValues = {1 - 2 * (q.y * q.y) - 2 * (q.z * q.z), 2 * q.x * q.y - 2 * q.w * q.z, 2 * q.x * q.z + 2 * q.w * q.y, 0,
                                    2 * q.x * q.y + 2 * q.w * q.z, 1 - 2 * (q.x * q.x) - 2 * (q.z * q.z), 2 * q.y * q.z - 2 * q.w * q.x, 0,
                                    2 * q.x * q.z - 2 * q.w * q.y, 2 * q.y * q.z + 2 * q.w * q.x, 1 - 2 * (q.x * q.x) - 2 * (q.y * q.y), 0,
                                    0, 0, 0, 1};
        float[] pValues = position.AsFloats();
        Matrix qRotationMatrix = new Matrix(4, 4, qRotationValues);
        Matrix pMatrix = new Matrix(4, 1, pValues);
        return (qRotationMatrix * pMatrix).AsCoords();
    }

    static public Matrix GetRotationMatrix(float angleX, bool clockwiseX,
                                           float angleY, bool clockwiseY,
                                           float angleZ, bool clockwiseZ)
    {
        if (clockwiseX)
        {
            angleX = 2 * Mathf.PI - angleX;
        }
        if (clockwiseY)
        {
            angleY = 2 * Mathf.PI - angleY;
        }
        if (clockwiseZ)
        {
            angleZ = 2 * Mathf.PI - angleZ;
        }

        float[] xrollValues = {1,0,0,0,
                               0,Mathf.Cos(angleX),-Mathf.Sin(angleX),0,
                               0,Mathf.Sin(angleX), Mathf.Cos(angleX),0,
                               0,0,0,1};
        Matrix XRoll = new Matrix(4, 4, xrollValues);

        float[] yrollValues = {Mathf.Cos(angleY),0,Mathf.Sin(angleY),0,
                               0,1,0,0,
                               -Mathf.Sin(angleY),0,Mathf.Cos(angleY),0,
                               0,0,0,1};
        Matrix YRoll = new Matrix(4, 4, yrollValues);

        float[] zrollValues = {Mathf.Cos(angleZ),-Mathf.Sin(angleZ),0,0,
                               Mathf.Sin(angleZ),Mathf.Cos(angleZ),0,0,
                               0,0,1,0,
                               0,0,0,1};
        Matrix ZRoll = new Matrix(4, 4, zrollValues);

        Matrix result = ZRoll * YRoll * XRoll;

        return result;
    }

    static public float GetRotationAxisAngle(Matrix rotation)
    {
        float angle = 0;
        angle = Mathf.Acos(0.5f * (rotation.GetValue(0, 0) +
                                    rotation.GetValue(1, 1) +
                                        rotation.GetValue(2, 2) +
                                           rotation.GetValue(3, 3) - 2));
        return angle;
    }

    static public Coords GetRotationAxis(Matrix rotation, float angle)
    {
        float vx = (rotation.GetValue(2, 1) - rotation.GetValue(1, 2)) / (2 * Mathf.Sin(angle));
        float vy = (rotation.GetValue(0, 2) - rotation.GetValue(2, 0)) / (2 * Mathf.Sin(angle));
        float vz = (rotation.GetValue(1, 0) - rotation.GetValue(0, 1)) / (2 * Mathf.Sin(angle));

        return new Coords(vx, vy, vz, 0);
    }

    static public Coords Shear(Coords position, float shearX, float shearY, float shearZ)
    {
        float[] shearValues = {1, shearY, shearZ, 0,
                                shearX, 1, shearZ, 0,
                                shearX, shearY, 1, 0,
                                0, 0, 0, 1};
        Matrix shearMatrix = new Matrix(4, 4, shearValues);
        Matrix pMatrix = new Matrix(4, 1, position.AsFloats());
        return (shearMatrix * pMatrix).AsCoords();
    }

    static public Coords ReflectX(Coords position)
    {
        float[] rValues = {-1, 0, 0, 0,
                        0, 1, 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1};
        Matrix rMatrix = new Matrix(4, 4, rValues);
        Matrix pMatrix = new Matrix(4, 1, position.AsFloats());
        return (rMatrix * pMatrix).AsCoords();
    }

    static public Coords Cross(Coords vector1, Coords vector2)
    {
        float xMult = vector1.y * vector2.z - vector1.z * vector2.y;
        float yMult = vector1.z * vector2.x - vector1.x * vector2.z;
        float zMult = vector1.x * vector2.y - vector1.y * vector2.x;
        Coords crossProd = new Coords(xMult, yMult, zMult);
        return crossProd;
    }
}
