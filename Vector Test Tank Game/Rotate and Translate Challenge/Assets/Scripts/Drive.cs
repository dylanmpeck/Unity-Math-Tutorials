using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        //transform.Translate(0, translation, 0);
        //Vector3 translationVector = this.transform.up * translation;
        this.transform.position = HolisticMath.Translate(new Coords(this.transform.position), new Coords(0, translation, 0), new Coords(transform.up)).ToVector();

        //transform.Rotate(0, 0, -rotation);
        transform.up = HolisticMath.Rotate(new Coords(transform.up), -rotation * Mathf.PI / 180.0f, false).ToVector();
    }
}
