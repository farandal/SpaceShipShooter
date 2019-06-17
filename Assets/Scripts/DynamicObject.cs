using System.Collections;
using System.Collections.Generic;
using System.IO;
using AsImpL;
using UnityEngine;

public class DynamicObject : MonoBehaviour {

    private ObjectImporter objImporter;
    public string filePath;
    public Vector3 rotation;
    public Vector3 position;
    public string name;
    public GameObject model;
    private bool loaded;
    private bool isFirstTime = true;
    private GameObject target;

    public GameObject[] objectsArr;
    private int objectsNumber;
    private float range = 5000;

    public void Setup (string name, string filePath, Vector3 rotation, Vector3 position,ref GameObject target,int objectsNumber = 1) {
        this.target = target;
        this.filePath = filePath;
        this.rotation = rotation;
        this.name = name;
        this.position = position;
        this.objectsNumber = objectsNumber;

        #if (UNITY_ANDROID || UNITY_IPHONE)
                this.filePath = Application.persistentDataPath + "/" + this.filePath;
        #endif
        
        if (!File.Exists (this.filePath)) {
            Debug.LogError ("Please set FilePath in ObjFromFile.cs to a valid path.");
            return;
        }

        ImportOptions importOptions = new ImportOptions ();
        ObjectImporter objImporter = gameObject.AddComponent<ObjectImporter> ();
        objImporter.ImportModelAsync (this.name, this.filePath, null, importOptions);
        objImporter.ImportedModel += this.OnModelImported;
    }

    private void OnModelImported (GameObject model, string absolutePath) {

        this.model = model;
        model.transform.SetParent (gameObject.transform, false);
        model.transform.position = this.position;
        this.loaded = true;

    }

    private void Update () {

        if (!this.loaded) {
            return;
        }

        if (this.isFirstTime) {

            this.objectsArr = new GameObject[this.objectsNumber];

            for (int i = 0; i <= this.objectsNumber -1; i++) {
                Vector3 newPosition = this.position + new Vector3 (Random.Range (-this.range, this.range), Random.Range (-this.range, this.range), Random.Range (-this.range + this.position.z, this.range + this.position.z));
                GameObject clone =  Instantiate (this.model, newPosition, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;  
                clone.transform.SetParent(gameObject.transform, false);
                clone.AddComponent<Pulse> ().Setup (2f);
                clone.AddComponent<RandomMovementTowardsTarget> ().Setup (this.target);
                 //objectsArr[i].AddComponent<LookAtTarget> ().Setup (ref this.target, this.rotation);  
                objectsArr[i] = clone;
               
            }

            this.model.SetActive (false);

        }

        this.isFirstTime = false;

    }

}