using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        //float translateX = Input.GetAxis("Horizontal") * speed;
        //float translateY = Input.GetAxis("VerticalY") * speed;
        float translateZ = Input.GetAxis("VerticalY") * speed;

        //transform.Translate(translateX, 0, 0);
        //transform.Translate(0, translateY, 0);


        float rotationX = Input.GetAxis("Vertical") * rotationSpeed;
        float rotationY = Input.GetAxis("Horizontal") * rotationSpeed;
        float rotationZ = Input.GetAxis("HorizontalZ") * rotationSpeed;

        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ));
        transform.Translate(0, 0, translateZ);
    }
}
