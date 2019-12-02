﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public Text energyAmt;
    Vector3 currentLocation;

    void Start()
    {
        currentLocation = this.transform.position;
    }

    void Update()
    {
        if (float.Parse(energyAmt.text) <= 0) return;

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        //transform.Rotate(0, 0, -rotation);

        energyAmt.text = (float.Parse(energyAmt.text) - Vector3.Distance(currentLocation,
                                             this.transform.position)) + "";

        currentLocation = this.transform.position;
    }
}