using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 


public class PlaneLaser : MonoBehaviour {

    //public ParticleSystem planeLaser;
    LineRenderer line;
    void Start() {
            //planeLaser = GetComponent<ParticleSystem>();
           
            line = gameObject.AddComponent<LineRenderer>();
             //line.transform.Translate (0f, 180f, 0f);
    }

    void Update() {

      if(Input.GetButton("Jump")) {
          StopCoroutine("FireLaser");
          StartCoroutine("FireLaser");
      }

    }

    IEnumerator FireLaser() {

        line.enabled = true;

        while(Input.GetButton("Jump")) {

            Ray ray = new Ray( transform.position, transform.up );
            RaycastHit hit;

            line.SetPosition(0, ray.origin);
            line.SetPosition(1, ray.GetPoint(500));
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.green;
            line.startWidth = 5f;
            line.endWidth = 0f;
            
            yield return null;
            //planeLaser.Emit(1);
        }

        line.enabled = false;

    }
    
}