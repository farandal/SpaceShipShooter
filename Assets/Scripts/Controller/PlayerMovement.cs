
using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour {

    float timeZeroToMax = 5f;
    float acceleration;
    public float velocity;
    public float maxSpeed = 10f;
     public float minSpeed = 0f;
    private int direction = 0;

    void Awake () {
        acceleration = maxSpeed / timeZeroToMax;
        velocity = 0f;
    }

    void Update () {

        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            this.direction = -1;
        }

        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            this.direction = 1;
        }

        if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
            velocity += direction * acceleration * Time.deltaTime;
        }

        if (Input.GetKey (KeyCode.UpArrow)) {
            velocity += acceleration * Time.deltaTime;
        }

        if (!Input.anyKey) {

            velocity = velocity < 0 ? velocity + (acceleration * Time.deltaTime) : velocity - (acceleration * Time.deltaTime);
        }

        velocity = Mathf.Clamp (velocity, minSpeed, maxSpeed);
        transform.Translate(Vector3.forward * (velocity));

    }

}