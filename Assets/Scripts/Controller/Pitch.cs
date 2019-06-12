using System;
using UnityEngine;
public class Pitch : MonoBehaviour {

    float timeZeroToMax = 1f;
    float acceleration;
    public float velocity;
    public float maxSpeed = 100;
    private int direction = 0;
    public float lerpAngle;
    public float lerpSpeed = 0;
    public float minAngle = -33;
    public float maxAngle = 33;

    void Awake () {
        acceleration = maxSpeed / timeZeroToMax;
        // acceleration = 0.1f;
        velocity = 0f;
        ForwardMovement.Init (gameObject);
    }

    public void SetAngles (float a, float b) {
        this.minAngle = a;
        this.maxAngle = b;
    }
    void Update () {

        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            this.direction = -1;
        }

        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            this.direction = 1;
        }
        float diff = (acceleration * Time.deltaTime);
        if (Input.GetAxis ("Vertical") != 0) {
            velocity += (Input.GetAxis ("Vertical") * 5) * diff;
        }

        if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
            velocity += direction * diff;
        } else {
            velocity = velocity < 0 ? velocity + diff : velocity - diff < 0 ? 0 : velocity - diff;

        }

        velocity = Mathf.Clamp (velocity, -maxSpeed, maxSpeed);

        lerpSpeed = ((velocity + maxSpeed) / 2) / maxSpeed;
        lerpAngle = Utils.lerp (minAngle, maxAngle, lerpSpeed);
        Debug.Log ("Pitch -velocity: " + velocity + " -lerpSpeed: " + lerpSpeed + " -lerpAngle: " + lerpAngle);

        ForwardMovement.Process (velocity);

    }

}