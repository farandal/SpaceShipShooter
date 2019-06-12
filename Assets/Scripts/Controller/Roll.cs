using System;
using UnityEngine;
public class Roll : MonoBehaviour {

    float timeZeroToMax = 1f;
    float acceleration;
    public float velocity;
    public float maxSpeed = 100;
    private int direction = 0;

    public float lerpAngle;
    public float lerpSpeed = 0;

    private float minAngle = -33;
    private float maxAngle = 33;

    void Awake () {
        //  acceleration = 0.1f;
        acceleration = maxSpeed / timeZeroToMax;
        velocity = 0f;

        SideMovement.Init (gameObject);
    }

    public void SetAngles (float a, float b) {
        this.minAngle = a;
        this.maxAngle = b;
    }

    void Update () {

        if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            this.direction = -1;
        }

        if (Input.GetKeyDown (KeyCode.RightArrow)) {
            this.direction = 1;
        }

        float diff = (acceleration * Time.deltaTime);

        if (Input.GetAxis ("Horizontal") != 0) {
            velocity += (Input.GetAxis ("Horizontal") * 5) * diff;
        }

        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
            velocity += direction * acceleration * Time.deltaTime;
        } else {
            velocity = velocity < 0 ? velocity + diff : velocity - diff < 0 ? 0 : velocity - diff;
        }

        velocity = Mathf.Clamp (velocity, -maxSpeed, maxSpeed);

        lerpSpeed = ((velocity + maxSpeed) / 2) / maxSpeed;
        lerpAngle = Utils.lerp (minAngle, maxAngle, lerpSpeed);
        Debug.Log ("Roll -velocity: " + velocity + " -lerpSpeed: " + lerpSpeed + " -lerpAngle: " + lerpAngle);

        SideMovement.Process (velocity);

    }

}