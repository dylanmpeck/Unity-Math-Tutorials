using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    Coords point = new Coords(10, 20);

    Coords vertStartPoint = new Coords(0, -100);
    Coords vertEndPoint = new Coords(0, 100);

    Coords horStartPoint = new Coords(-160, 0);
    Coords horEndPoint = new Coords(160, 0);

    Coords[] libraSignPoints = {
        new Coords(0, 22),
        new Coords(9.9f, 37.5f),
        new Coords(0.8f, 54.4f),
        new Coords(30, 60.4f),
        new Coords(57.5f, 42.4f),
        new Coords(53.3f, 17)
    };

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(point.ToString());
        //Coords.DrawPoint(point, 2.0f, Color.green);

        Coords.DrawLine(vertStartPoint, vertEndPoint, 0.5f, Color.green);
        Coords.DrawLine(horStartPoint, horEndPoint, 0.5f, Color.red);

        foreach (Coords c in libraSignPoints)
        {
            Coords.DrawPoint(c, 2, Color.yellow);
        }

        for (int i = 1; i < libraSignPoints.Length; i++)
        {
            Coords.DrawLine(libraSignPoints[i - 1], libraSignPoints[i], 0.5f, Color.white);
        }

        //Bottom of triangle. Only line that isn't in a row.
        Coords.DrawLine(libraSignPoints[2], libraSignPoints[4], 0.5f, Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
