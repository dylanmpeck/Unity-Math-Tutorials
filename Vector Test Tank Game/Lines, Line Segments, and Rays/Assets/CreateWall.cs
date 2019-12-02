using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    Line wall;
    Line ballPath;
    public GameObject ball;
    float intersectT;
    float intersectS;

    // Start is called before the first frame update
    void Start()
    {
        wall = new Line(new Coords(5, -2, 0), new Coords(0, 5, 0));
        wall.Draw(1, Color.blue);

        ballPath = new Line(new Coords(-6, 0, 0), new Coords(100, 0, 0));
        ballPath.Draw(0.1f, Color.yellow);

        ball.transform.position = ballPath.A.ToVector();

        intersectT = ballPath.IntersectsAt(wall);
        intersectS = wall.IntersectsAt(ballPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (intersectT != intersectT || intersectS != intersectS)
            ball.transform.position = ballPath.Lerp(Time.time * 0.1f).ToVector();
        else if (intersectT == intersectT && intersectS == intersectS && Time.time * 0.1f <= intersectT)
            ball.transform.position = ballPath.Lerp(Time.time * 0.1f).ToVector();
    }
}
