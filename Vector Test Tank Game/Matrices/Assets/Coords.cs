﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coords {

    public float x;
    public float y;
    public float z;
    public float w;

    public Coords(float _X, float _Y)
    {
        x = _X;
        y = _Y;
        z = -1;
    }

    public Coords(float _X, float _Y, float _Z)
    {
        x = _X;
        y = _Y;
        z = _Z;
    }

    public Coords(float _X, float _Y, float _Z, float _W)
    {
        x = _X;
        y = _Y;
        z = _Z;
        w = _W;
    }

    public Coords(Vector3 vecpos)
    {
        x = vecpos.x;
        y = vecpos.y;
        z = vecpos.z;
    }

    public Coords(Vector3 vecpos, float _W)
    {
        x = vecpos.x;
        y = vecpos.y;
        z = vecpos.z;
        w = _W;
    }

    public float[] AsFloats()
    {
        float[] values = { x, y, z, w };
        return values;
    }

    public Coords GetNormal()
    {
        float magnitude = HolisticMath.Distance(new Coords(0, 0, 0), new Coords(x, y, z));
        return new Coords(x / magnitude, y / magnitude, z / magnitude);
    }

    public override string ToString()
    {
        return"(" + x + "," + y + "," + z +")";
    }

    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }

    static public Coords operator+ (Coords a, Coords b)
    {
        Coords c = new Coords(a.x + b.x, a.y + b.y, a.z + b.z);
        return c;
    }

    static public Coords operator- (Coords a, Coords b)
    {
        Coords c = new Coords(a.x - b.x, a.y - b.y, a.z - b.z);
        return c;
    }

    static public Coords operator *(Coords a, float b)
    {
        Coords c = new Coords(a.x * b, a.y * b, a.z * b);
        return c;
    }

    static public Coords operator /(Coords a, float b)
    {
        Coords c = new Coords(a.x / b, a.y / b, a.z / b);
        return c;
    }

    static public Coords Perp(Coords v)
    {
        return new Coords(-v.y, v.x, 0);
    }

    static public void DrawLine(Coords startPoint, Coords endPoint, float width, Color colour)
    {
        GameObject line = new GameObject("Line_" + startPoint.ToString() + "_" + endPoint.ToString());
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = colour;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(startPoint.x, startPoint.y, startPoint.z));
        lineRenderer.SetPosition(1, new Vector3(endPoint.x, endPoint.y, endPoint.z));
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    static public void DrawPoint(Coords position, float width, Color colour)
    {
        GameObject line = new GameObject("Point_" + position.ToString());
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = colour;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(position.x - width / 3.0f, position.y - width / 3.0f, position.z));
        lineRenderer.SetPosition(1, new Vector3(position.x + width / 3.0f, position.y + width / 3.0f, position.z));
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

}
