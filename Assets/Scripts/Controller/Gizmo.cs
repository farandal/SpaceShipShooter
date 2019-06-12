using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour {

    private bool XAxisEnabled, YAxisEnabled, ZAxisEnabled = false;

    private Vector3 xAxisRotationVect = new Vector3 (0f, 90f, 90f);
    private Vector3 yAxisRotationVect = new Vector3 (180f, 0f, 90f);
    private Vector3 zAxisRotationVect = new Vector3 (90f, -180f, 0f);

    private Vector3 correctionVector = new Vector3 (0f, 0f, 180f);
    private float objectRadius;
    public GameObject xAxis, yAxis, zAxis;
    private GameObject target;
    private Roll roll;
    private Pitch pitch;
    
    private Jaw jaw;
    private float speedFactor = 100f;

    public Quat quat = new Quat (0, 0, 0, -1);
    public void Config (GameObject target, float objectRadius, Vector3 correctionVector, Vector3 axis) {
        this.target = target;
        this.correctionVector = correctionVector;
        this.objectRadius = objectRadius;
        this.XAxisEnabled = axis.x == 1 ? true : false;
        this.YAxisEnabled = axis.y == 1 ? true : false;
        this.ZAxisEnabled = axis.z == 1 ? true : false;
    }

    // Start is called before the first frame update
    void Start () {

        if (this.target == null) {
            return;
        }

        xAxis = new GameObject { name = "xAxis" };
        xAxis.DrawCircle (objectRadius, 2f, Color.red, xAxisRotationVect);
        //xAxis.transform.rotation = Quaternion.Euler(xAxisRotationVect); 
        //xAxis.transform.Rotate (yAxisRotationVect,Space.Self);
        xAxis.transform.position = transform.position;
        xAxis.transform.SetParent (transform, true);

        yAxis = new GameObject { name = "yAxis" };
        yAxis.DrawCircle (objectRadius, 2f, Color.green, yAxisRotationVect);
        //yAxis.transform.Rotate (yAxisRotationVect,Space.Self);
        //yAxis.transform.rotation = Quaternion.Euler(yAxisRotationVect); 
        yAxis.transform.position = transform.position;
        yAxis.transform.SetParent (transform, true);

        zAxis = new GameObject { name = "zAxis" };
        zAxis.DrawCircle (objectRadius, 2f, Color.blue, zAxisRotationVect);
        //zAxis.transform.Rotate (zAxisRotationVect,Space.Self);
        //zAxis.transform.rotation = Quaternion.Euler(zAxisRotationVect); 
        zAxis.transform.position = transform.position;
        zAxis.transform.SetParent (transform, true);

  
        roll = gameObject.AddComponent<Roll> () as Roll;
        roll.SetAngles (-33,33);

        jaw = gameObject.AddComponent<Jaw> () as Jaw;
        jaw.SetAngles (-16,16);

        pitch = gameObject.AddComponent<Pitch> () as Pitch;
        pitch.SetAngles (16,-16);
        

    }

    // Update is called once per frame
    void Update () {

        StopCoroutine ("Calculate");
        StartCoroutine ("Calculate");

    }

    IEnumerator Calculate () {

/* 
        Quat Qjaw = this.XAxisEnabled ? new Quat (Mathf.Cos (jaw.velocity / speedFactor), 0, Mathf.Sin (jaw.velocity / speedFactor), 0) : new Quat (0, 0, 0, -1);
        Quat Qroll = this.YAxisEnabled ? new Quat (Mathf.Cos (roll.velocity / speedFactor), Mathf.Sin (roll.velocity / speedFactor), 0, 0) : new Quat (0, 0, 0, -1);
        Quat Qpitch = this.ZAxisEnabled ? new Quat (Mathf.Cos (pitch.velocity / speedFactor), 0, 0, Mathf.Sin (pitch.velocity / speedFactor)) : new Quat (0, 0, 0, -1);

        if (this.XAxisEnabled) { quat = Quat.Multiply (Qroll, quat); }

        if (this.ZAxisEnabled) { quat = Quat.Multiply (Qpitch, quat); }
        if (this.ZAxisEnabled) { quat = Quat.Multiply (Qjaw, quat); }
*/
  
        Quaternion output = Quaternion.Euler (-pitch.lerpAngle,-roll.lerpAngle,-jaw.lerpAngle);
        Quaternion correction = Quaternion.Euler (this.correctionVector);

        this.target.transform.rotation = correction *= output;

        if (this.XAxisEnabled) { xAxis.transform.rotation = output *= Quaternion.Euler (this.xAxisRotationVect); }
        if (this.YAxisEnabled) { yAxis.transform.rotation = output *= Quaternion.Euler (this.yAxisRotationVect); }
        if (this.ZAxisEnabled) { zAxis.transform.rotation = output *= Quaternion.Euler (this.zAxisRotationVect); }

        yield return null;

    }
}