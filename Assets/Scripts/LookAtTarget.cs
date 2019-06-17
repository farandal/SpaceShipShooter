using System.Collections;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    private GameObject target = null;

    private float minSpeed = 1f;
    private float maxSpeed = 5f;
    private float speed = 0f;

    public Vector3 targetDirection;
    public Vector3 targetPosition;
    public Vector3 currentosition;
    public Vector3 rotationCorrection;

    public void Setup (ref GameObject target, Vector3 rotationCorrection) {
        this.target = target;
        this.rotationCorrection = rotationCorrection;
        this.speed = Random.Range (this.minSpeed, this.maxSpeed);
    }

    void Update () {
        
        if(!this.target){
            return;
        }
        this.targetPosition = this.target.transform.position;
        this.currentosition = gameObject.transform.position;
        this.targetDirection =  gameObject.transform.position - this.target.transform.position ;

        Quaternion correction = Quaternion.Euler (this.rotationCorrection);
        gameObject.transform.rotation = correction *= Quaternion.LookRotation (this.targetDirection);

        //this.targetDirection =  gameObject.transform.position - this.target.transform.position ;
        gameObject.DrawLine (this.target.transform.position, Color.red, 1f, 1f);
    }

}