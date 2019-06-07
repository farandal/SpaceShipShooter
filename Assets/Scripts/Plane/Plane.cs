﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using AsImpL;
using UnityEngine;

public class Plane : MonoBehaviour {

    private ObjectImporter objImporter;

 
    // Start is called before the first frame update
    void Start () {
        ImportPlane ();
    }

    // Update is called once per frame
    void Update () {

    }

    private void ImportPlane () {

        string filePath = "Assets/Resources/plane/plane.obj";

#if (UNITY_ANDROID || UNITY_IPHONE)
        filePath = Application.persistentDataPath + "/" + filePath;
#endif

        if (!File.Exists (filePath)) {
            Debug.LogError ("Please set FilePath in ObjFromFile.cs to a valid path.");
            return;
        }

        ImportOptions importOptions = new ImportOptions ();
        ObjectImporter objImporter = gameObject.AddComponent<ObjectImporter> ();
        objImporter.ImportModelAsync ("Plane", filePath, null, importOptions);
        objImporter.ImportedModel += this.OnPlaneImported;

    }

    private void OnPlaneImported (GameObject plane, string absolutePath) {

        Vector3 correctionVector = new Vector3 (90f, 0f, 0f);

      

        //playerContainer.transform.position = new Vector3 (0f, playerAltitud, 0f);
        //playerContainer.transform.Translate (0f,playerAltitud, 0f);

        plane.transform.SetParent (gameObject.transform, false);

        Gizmo gizmo = gameObject.AddComponent<Gizmo> ();
        gizmo.SetCorrectionVector (correctionVector);
        gizmo.SetTarget (plane);

        GameObject buster1 = GameObject.Instantiate (GameObject.Find ("Particles/Burster")) as GameObject;
        GameObject buster2 = GameObject.Instantiate (GameObject.Find ("Particles/Burster")) as GameObject;

        plane.AddComponent<PlaneLaser> ();

        GameObject weapon = GameObject.Instantiate (GameObject.Find ("Particles/Weapon")) as GameObject;
        weapon.transform.SetParent (plane.transform, false);

        weapon.AddComponent<PlaneWeapon> ();
        weapon.transform.Translate (0f, 0f, 180f);
        weapon.transform.Rotate (new Vector3 (0f, 0f, 0f), Space.World);

        buster1.transform.Translate (-75f, -100f, 0f);
        buster2.transform.Translate (80f, -100f, -0f);

        buster1.transform.Rotate (correctionVector, Space.Self);
        buster2.transform.Rotate (correctionVector, Space.Self);

        buster1.transform.SetParent (plane.transform, false);
        buster2.transform.SetParent (plane.transform, false);

        plane.AddComponent<Pulse> ();
        //playerContainer.transform.localScale = new Vector3(0.1f,0.1f, 0.1f);

    }

}