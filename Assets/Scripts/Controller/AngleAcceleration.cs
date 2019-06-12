using System;
using UnityEngine;
public class AngleAcceleration : MonoBehaviour {

    float timeZeroToMax = 0.5f;
    float acceleration;
    public float velocity;
    public float maxSpeed = 100f;
    private int direction = 0;

    private float lerpAngle;
    private float lerpSpeed = 0;

    public float minAngle = -33;
    public float maxAngle = 33;

    void Awake () {
        acceleration = maxSpeed / timeZeroToMax;
        velocity = 0f;
    }

    public void SetAngles (float a, float b) {
        this.minAngle = a;
        this.maxAngle = b;
    }

    static float lerp (float a, float b, float f) {
        return a + f * (b - a);
    }

    void Update () {

        if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            this.direction = -1;
        }

        if (Input.GetKeyDown (KeyCode.RightArrow)) {
            this.direction = 1;
        }

        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
            velocity += direction * acceleration * Time.deltaTime;
        }

        if (Input.GetKey (KeyCode.RightArrow)) {
            velocity += acceleration * Time.deltaTime;
        }

        if (!Input.anyKey) {

            velocity = velocity < 0 ? velocity + (acceleration * Time.deltaTime) : velocity - (acceleration * Time.deltaTime);
        }

        velocity = Mathf.Clamp (velocity, -maxSpeed, maxSpeed);
        lerpSpeed = ((velocity + maxSpeed) / 2) / maxSpeed;
        lerpAngle = AngleAcceleration.lerp (minAngle, maxAngle, lerpSpeed);
        Debug.Log ("-velocity: " + velocity + " -lerpSpeed: " + lerpSpeed + " -lerpAngle: " + lerpAngle);

    }

}