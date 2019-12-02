using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCreate : MonoBehaviour
{
    public GameObject start;
    public GameObject end1;
    public GameObject end2;
    Plane plane;

    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(new Coords(start.transform.position),
                          new Coords(end1.transform.position),
                          new Coords(end2.transform.position));

        for (float i = 0.0f; i <= 1.0f; i += 0.1f)
        {
            for (float j = 0.0f; j <= 1.0f; j += 0.1f)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.localScale *= 0.5f;
                sphere.transform.position = plane.Lerp(i, j).ToVector();
            }
        }
    }
}
