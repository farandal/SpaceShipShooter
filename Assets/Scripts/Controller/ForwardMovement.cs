using System;
using UnityEngine;
public class ForwardMovement {

    public static float timeZeroToMax = 10f;
    public static float acceleration;

    public static float maxSpeed = 10f;
     public static float minSpeed = 0;

    public static GameObject target;

    private static Vector3 orientation = Vector3.forward;

    public static void Init (GameObject target) {
        ForwardMovement.acceleration = ForwardMovement.maxSpeed / ForwardMovement.timeZeroToMax;
        ForwardMovement.target = target;
    }

    public static void Process (float velocity) {

        velocity = Mathf.Clamp (velocity, ForwardMovement.minSpeed, ForwardMovement.maxSpeed);
       
        Debug.Log ("ForwardMovement: " + velocity);
        ForwardMovement.target.transform.Translate (ForwardMovement.orientation * (velocity));

    }

}