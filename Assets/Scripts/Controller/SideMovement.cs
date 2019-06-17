using System;
using UnityEngine;
public class SideMovement {

    public static float timeZeroToMax = 10f;
    public static float acceleration;

    public static float maxSpeed = 10f;

    private static Vector3 orientation = new Vector3 (1f, 0f, 0f);

    private static GameObject target;

    public static void Init (GameObject target) {
        SideMovement.acceleration = SideMovement.maxSpeed / SideMovement.timeZeroToMax;
        SideMovement.target = target;
    }

    public static void Process (float velocity) {
        velocity = Mathf.Clamp (velocity, -SideMovement.maxSpeed, SideMovement.maxSpeed);
        Debug.Log ("SideMovement: " + velocity+ " Orientation: "+SideMovement.orientation);
        SideMovement.target.transform.position = new Vector3(SideMovement.target.transform.position.x + velocity,SideMovement.target.transform.position.y,SideMovement.target.transform.position.z);
    }

}