using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateE : MonoBehaviour
{
    public Vector3 eulerAngles;
    Matrix rotationMatrix;
    float angle;
    Coords axis;

    private void Start()
    {
        rotationMatrix = HolisticMath.GetRotationMatrix(eulerAngles.x * Mathf.Deg2Rad, false,
                                                        eulerAngles.y * Mathf.Deg2Rad, false,
                                                        eulerAngles.z * Mathf.Deg2Rad, false);
        angle = HolisticMath.GetRotationAxisAngle(rotationMatrix);
        axis = HolisticMath.GetRotationAxis(rotationMatrix, angle);
    }

    // Update is called once per frame
    void Update()
    {
        /*this.transform.forward = HolisticMath.Rotate(new Coords(this.transform.forward, 0),
                                                     1 * Mathf.Deg2Rad, false,
                                                     1 * Mathf.Deg2Rad, false,
                                                     1 * Mathf.Deg2Rad, false).ToVector();*/

        Coords quaternion = HolisticMath.Quaternion(axis, angle);
        this.transform.rotation *= new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }
}
