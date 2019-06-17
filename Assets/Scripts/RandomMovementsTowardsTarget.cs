using System.Collections;
using System.Collections.Generic;
using System.IO;
using AsImpL;
using UnityEngine;

public class RandomMovementTowardsTarget : MonoBehaviour {

    private GameObject target = null;
    //public Vector3 currentPosition = new Vector3(0,0,0);
    public float speed = 5f;

    public void Setup (GameObject target) {
        this.target = target;
        //this.currentPosition = gameObject.transform.position;
        this.speed = Random.Range (5f, 20f);
    }

    private void Update () {
        
        if(!this.target) {
            return;
        }
   
        float step = speed;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,  this.target.transform.position,  this.speed);
        Quaternion correction = Quaternion.Euler (new Vector3(-90,0,0));
        Vector3 targetDir = this.target.transform.position - gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(gameObject.transform.forward, targetDir, this.speed,1000f);
        //gameObject.DrawLine (newDir, Color.blue,5f,5f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        gameObject.transform.rotation  = correction *= Quaternion.LookRotation (newDir);


    }

}