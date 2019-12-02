using UnityEngine;
using System.Collections;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    float speed = 5.0f;
    public GameObject fuel;
    Vector3 direction;
    float stoppingDistance = 0.1f;


    private void Start()
    {
        direction = fuel.transform.position - this.transform.position;
        Coords dirNormal = HolisticMath.GetNormal(new Coords(direction));
        direction = dirNormal.ToVector();

        this.transform.up = HolisticMath.LookAt2D(this.transform.up,
                                                  new Coords(this.transform.position),
                                                  new Coords(fuel.transform.position)).ToVector();
    }

    void Update()
    {
        if (HolisticMath.Distance(new Coords(this.transform.position),
                                  new Coords(fuel.transform.position)) > stoppingDistance)
            this.transform.position += direction * speed * Time.deltaTime;
    }
}