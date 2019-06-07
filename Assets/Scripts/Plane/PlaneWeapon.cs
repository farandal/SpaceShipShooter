using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneWeapon : MonoBehaviour {

    public ParticleSystem planeWeapon;

    void Start () {
        planeWeapon = gameObject.GetComponent<ParticleSystem> ();
       // planeWeapon.transform.Translate (0f, 180f, 0f);

    }

    void Update () {

        if (Input.GetButton ("Jump")) {
            StopCoroutine ("FireWeapon");
            StartCoroutine ("FireWeapon");
             

        }

    }

    IEnumerator FireWeapon () {

        while (Input.GetButton ("Jump")) {

            planeWeapon.Emit (1);

            yield return null;

        }

    }

}