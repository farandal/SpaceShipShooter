using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AcceleratedValue : MonoBehaviour {

    float timeZeroToMax = 1f;
    float acceleration;
    public float velocity;
    public Vector3 angle;
    public Quaternion rotation;
    float maxSpeed = 1f;
    private int orientation;
    private int axis;
    private Vector3 orientationVector;

    private float rotationAxis;

    public enum Orientation {
        Roll = 0,
        Pitch = 1,
        Jaw = 2,
    }

    public enum Axis {
        x = 1,
        y = 2,
        z = 3
    }

    void Awake () {
        acceleration = maxSpeed / timeZeroToMax;
        velocity = 0f;
       
    }

    public void config (Orientation orientation, Axis axis) {

        this.orientation = (int) orientation;
        this.axis = (int) axis;

        switch (axis) {

            case Axis.x:
                {
                    this.orientationVector = new Vector3 (-1, 0, 0);

                    break;
                }
            case Axis.y:
                {
                    this.orientationVector = new Vector3 (0, 1, 0);

                    break;
                }
            case Axis.z:
                {
                    this.orientationVector = new Vector3 (0, 0, 1);

                    break;
                }

        }

    }

    void Update () {
        
        if (orientation == (int) Orientation.Roll) {
            if (Input.GetKey (KeyCode.LeftArrow)) {
                velocity -= acceleration * Time.deltaTime;
            }

            if (Input.GetKey (KeyCode.RightArrow)) {
                velocity += acceleration * Time.deltaTime;
            }

            if (!Input.anyKey) {
                velocity = velocity < 0 ? velocity + (acceleration * Time.deltaTime) : velocity - (acceleration * Time.deltaTime);
            }
        }

        if (orientation == (int) Orientation.Pitch) {

            if (Input.GetKey (KeyCode.UpArrow)) {
                velocity -= acceleration * Time.deltaTime;
            }

            if (Input.GetKey (KeyCode.DownArrow)) {
                velocity += acceleration * Time.deltaTime;
            }

            if (!Input.anyKey) {
                velocity = velocity < 0 ? velocity + (acceleration * Time.deltaTime) : velocity - (acceleration * Time.deltaTime);
            }

        }

        if (orientation == (int) Orientation.Jaw) {

            if (Input.GetKey (KeyCode.A)) {
                velocity -= acceleration * Time.deltaTime;
            }

            if (Input.GetKey (KeyCode.D)) {
                velocity += acceleration * Time.deltaTime;
            }

            if (!Input.anyKey) {
                velocity = velocity < 0 ? velocity + (acceleration * Time.deltaTime) : velocity - (acceleration * Time.deltaTime);
            }

        }

        velocity = Mathf.Clamp (velocity, -maxSpeed, maxSpeed);
        //transform.Rotate (orientationVector * velocity);
        
    }

}