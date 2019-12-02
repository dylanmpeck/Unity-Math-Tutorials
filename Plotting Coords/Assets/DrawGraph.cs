using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGraph : MonoBehaviour
{
    public int size = 20;

    public int xMax = 200;
    public int yMax = 200;

    Coords vertStartPoint;
    Coords vertEndPoint;

    Coords horStartPoint;
    Coords horEndPoint;

    // Start is called before the first frame update
    void Start()
    {
        //xWidth = (int)(Camera.main.aspect * Camera.main.orthographicSize);
        //yHeight = (int)(Camera.main.orthographicSize);

        vertStartPoint = new Coords(0, -yMax);
        vertEndPoint = new Coords(0, yMax);

        horStartPoint = new Coords(-xMax, 0);
        horEndPoint = new Coords(xMax, 0);

        Coords.DrawLine(vertStartPoint, vertEndPoint, 1.0f, Color.green);
        Coords.DrawLine(horStartPoint, horEndPoint, 1.0f, Color.red);

        drawHorizontalLines();
        drawVerticalLines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void drawHorizontalLines()
    {
        int yGridOffset = (int)(yMax / (float)size);
        for (int yPos = -(yGridOffset * size); yPos <= yGridOffset * size; yPos += size)
        {
            Coords.DrawLine(new Coords(-xMax, yPos), new Coords(xMax, yPos), 0.5f, Color.white);
        }
    }

    void drawVerticalLines()
    {
        int xGridOffset = (int)(xMax / (float)size);
        for (int xPos = -(xGridOffset * size); xPos <= xGridOffset * size; xPos += size)
        {
            Coords.DrawLine(new Coords(xPos, -yMax), new Coords(xPos, yMax), 0.5f, Color.white);
        }
    }
}
