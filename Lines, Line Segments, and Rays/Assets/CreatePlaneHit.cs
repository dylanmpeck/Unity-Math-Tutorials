using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlaneHit : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;
    public Transform E;

    Plane plane;
    Line L1;

    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(new Coords(A.position), new Coords(B.position), new Coords(C.position));
        L1 = new Line(new Coords(D.position), new Coords(E.position), Line.LINETYPE.RAY);
        L1.Draw(1.0f, Color.green);

        for (float s = 0; s <= 1.0f; s += 0.1f)
        {
            for (float t = 0; t <= 1.0f; t += 0.1f)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = plane.Lerp(s, t).ToVector();
            }
        }

        float intersectT = L1.IntersectsAt(plane);
        if (intersectT == intersectT)
        {
            GameObject intersectCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            intersectCube.name = "HitPoint";
            intersectCube.transform.position = L1.Lerp(intersectT).ToVector();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
