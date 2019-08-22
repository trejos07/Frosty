using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour, IController
{
    [Range(0, 2)] private float sensitivity=1;
    private Quaternion calibrationQuaternion;

    private void Start()
    {
        CalibrateAccelerometer();
    }

    public float MoveHorizontal() //Applies the interface command to move with phone gyroscope
    {
        //print(Input.acceleration);
        return FixAcceleration(Input.acceleration).x;
    }

    private void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapShot = Input.acceleration;
        print("the phone start Rot is " + accelerationSnapShot);
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapShot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    private Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration * sensitivity;
        return fixedAcceleration;

    }

}
