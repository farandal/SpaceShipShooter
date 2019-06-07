using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour {

    private Vector3 xAxisRotationVect = new Vector3 (0f, 0f, 0f);
    private Vector3 zAxisRotationVect = new Vector3 (0f, 90f, 90f);
    private Vector3 yAxisRotationVect = new Vector3 (0f, 0f, 0f);
    private Vector3 correctionVector = new Vector3 (0f, 0f, 0f);
    float objectRadius = 180f;

    public GameObject xAxis, yAxis, zAxis;
    private GameObject target;
    private AcceleratedValue roll, pitch, jaw;

    private float speedFactor = 30f;

    public Quat quat = new Quat (0, 0, 0, -1);
    public void SetTarget (GameObject target) {
        this.target = target;
    }

    public void SetCorrectionVector (Vector3 v) {
        this.correctionVector = v;
    }
    // Start is called before the first frame update
    void Start () {

        xAxis = new GameObject { name = "xAxis" };
        xAxis.DrawCircle (objectRadius, 2f, Color.red);
        xAxis.transform.Rotate (xAxisRotationVect, Space.Self);
        xAxis.transform.SetParent (gameObject.transform, false);
        xAxis.layer = 3;

        yAxis = new GameObject { name = "yAxis" };
        yAxis.DrawCircle (objectRadius, 2f, Color.green);
        yAxis.transform.Rotate (yAxisRotationVect, Space.Self);
        yAxis.transform.SetParent (gameObject.transform, false);
        yAxis.layer = 3;

        zAxis = new GameObject { name = "zAxis" };
        zAxis.DrawCircle (objectRadius, 2f, Color.blue);
        zAxis.transform.Rotate (zAxisRotationVect, Space.Self);
        zAxis.transform.SetParent (gameObject.transform, false);
        zAxis.layer = 3;

        roll = gameObject.AddComponent<AcceleratedValue> () as AcceleratedValue;
        roll.config (AcceleratedValue.Orientation.Roll, AcceleratedValue.Axis.y);

        pitch = gameObject.AddComponent<AcceleratedValue> () as AcceleratedValue;
        pitch.config (AcceleratedValue.Orientation.Pitch, AcceleratedValue.Axis.z);

        jaw = gameObject.AddComponent<AcceleratedValue> () as AcceleratedValue;
        jaw.config (AcceleratedValue.Orientation.Jaw, AcceleratedValue.Axis.x);

    }

    // Update is called once per frame
    void Update () {

        StopCoroutine ("Calculate");
        StartCoroutine ("Calculate");

    }

    IEnumerator Calculate () {

        Quat Qpitch = new Quat (Mathf.Cos (pitch.velocity / speedFactor), 0, 0, Mathf.Sin (pitch.velocity / speedFactor));
        Quat Qjaw = new Quat (Mathf.Cos (jaw.velocity / speedFactor), 0, Mathf.Sin (jaw.velocity / speedFactor), 0);
        Quat Qroll = new Quat (Mathf.Cos (roll.velocity / speedFactor), Mathf.Sin (roll.velocity / speedFactor), 0, 0);

        quat = Quat.Multiply (Qpitch, quat);
        quat = Quat.Multiply (Qjaw, quat);
        quat = Quat.Multiply (Qroll, quat);

        Quaternion output = quat.ToUnityQuaternion ();

        this.target.transform.rotation = output *= Quaternion.Euler (this.correctionVector);

        xAxis.transform.rotation = output *= Quaternion.Euler (new Vector3 (0, 0, 0));
        yAxis.transform.rotation = output *= Quaternion.Euler (new Vector3 (0, 0, 90));
        zAxis.transform.rotation = output *= Quaternion.Euler (new Vector3 (90, 0, 0));

        yield return null;

    }
}