using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private float factor = 1000f;
	// Use this for initialization
	void Start () {
     
	}

    void Update()
    {
       // movementValue = Input.GetAxis("Vertical");
       // turnValue = Input.GetAxis("Horizontal");

        if (Input.GetKey("up"))
        {
             transform.Translate(Vector3.forward * factor * Time.deltaTime);
        }

        if (Input.GetKey("down"))
        {
             transform.Translate(Vector3.back * factor * Time.deltaTime);
        }

    }

}
