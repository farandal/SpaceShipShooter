using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
{
    RenderSettings.fog =true;
    RenderSettings.fogDensity = 0.01f;
    //StartCoroutine(updateTheFog());
}

IEnumerator updateTheFog() 
{
    while(true)
    { 
        //this makes the loop itself yield 
        new WaitForSeconds(1); 

        RenderSettings.fogDensity+=0.01f;

          yield return null;
    }
    //if you want to stop the loop, use: break;
}
}
