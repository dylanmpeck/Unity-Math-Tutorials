using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSolidWall : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;
    public Transform E;

    public Transform ball;
    Plane plane;
    Line L1;
    Line trajectory;
    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(new Coords(A.position),
                          new Coords(B.position),
                          new Coords(C.position));

        for (float s = 0; s <= 1.0f; s += 0.1f)
        {
            for (float t = 0; t <= 1.0f; t += 0.1f)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = plane.Lerp(s, t).ToVector();
            }
        }

        L1 = new Line(new Coords(D.position), new Coords(E.position), Line.LINETYPE.RAY);
        //L1.Draw(1.0f, Color.green);

        ball.transform.position = L1.A.ToVector();

        float intersectT = L1.IntersectsAt(plane);

        if (intersectT == intersectT)
        {
            trajectory = new Line(L1.A, L1.Lerp(intersectT), Line.LINETYPE.SEGMENT);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 1.0f)
            ball.transform.position = trajectory.Lerp(Time.time).ToVector();
        else
            ball.transform.position += trajectory.Reflect(HolisticMath.Cross(plane.v, plane.u)).ToVector() * Time.deltaTime * 10.0f;
    }
}
