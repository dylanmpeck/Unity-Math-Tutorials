using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
    public GameObject point;
    public GameObject[] points;
    public Vector3 angle;
    public Vector3 translation;
    public Vector3 scaling;
    public Vector3 shear;

    public GameObject center;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 c = new Vector3(center.transform.position.x,
                                center.transform.position.y,
                                center.transform.position.z);

        angle = new Vector3(angle.x * Mathf.Deg2Rad, angle.y * Mathf.Deg2Rad, angle.z * Mathf.Deg2Rad);

        //point.transform.position = HolisticMath.Translate(position, new Coords(new Vector3(translation.x, translation.y, translation.z), 0)).ToVector();
        foreach (GameObject sphere in points)
        {
            
            Coords position = new Coords(sphere.transform.position, 1);
            sphere.transform.position = HolisticMath.ReflectX(position).ToVector();

            //sphere.transform.position = HolisticMath.Shear(position, shear.x, shear.y, shear.z).ToVector();

            //position = HolisticMath.Translate(position, new Coords(new Vector3(-c.x, -c.y, -c.z), 0));
            //position = HolisticMath.Rotate(position, angle.x, true, angle.y, true, angle.z, true);
            //sphere.transform.position = HolisticMath.Translate(position, new Coords(new Vector3(c.x, c.y, c.z), 0)).ToVector();

            //sphere.transform.position = HolisticMath.Translate(position, new Coords(new Vector3(translation.x, translation.y, translation.z), 0)).ToVector();

            //position = HolisticMath.Translate(position, new Coords(new Vector3(-c.x, -c.y, -c.z), 0));
            //position = HolisticMath.Scale(position, scaling.x, scaling.y, scaling.z);
            //sphere.transform.position = HolisticMath.Translate(position, new Coords(new Vector3(c.x, c.y, c.z), 0)).ToVector();
        }
        DrawHouseLines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawHouseLines()
    {
        Coords.DrawLine(new Coords(points[0].transform.position), new Coords(points[1].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[0].transform.position), new Coords(points[2].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[0].transform.position), new Coords(points[4].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[1].transform.position), new Coords(points[3].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[1].transform.position), new Coords(points[5].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[2].transform.position), new Coords(points[3].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[5].transform.position), new Coords(points[4].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[2].transform.position), new Coords(points[6].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[3].transform.position), new Coords(points[7].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[6].transform.position), new Coords(points[7].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[0].transform.position), new Coords(points[9].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[4].transform.position), new Coords(points[9].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[6].transform.position), new Coords(points[8].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[2].transform.position), new Coords(points[8].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[4].transform.position), new Coords(points[6].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[5].transform.position), new Coords(points[7].transform.position), 0.1f, Color.yellow);
        Coords.DrawLine(new Coords(points[8].transform.position), new Coords(points[9].transform.position), 0.1f, Color.yellow);
    }
}
