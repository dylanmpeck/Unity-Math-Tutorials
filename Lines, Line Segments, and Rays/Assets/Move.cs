﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform start;
    public Transform end;
    Line line;

    // Start is called before the first frame update
    void Start()
    {
        //line = new Line(new Coords(start.position), new Coords(end.position), Line.LINETYPE.SEGMENT);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = line.GetPointAt(Time.time * 0.01f).ToVector();
        this.transform.position = HolisticMath.Lerp(new Coords(start.position),
                                                    new Coords(end.position),
                                                    Mathf.Sin(Time.time) * 1).ToVector();
    }
}
 