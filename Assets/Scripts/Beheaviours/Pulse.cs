using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{

  void Start()
  {

  }

  void Update()
  {

    PulseRoutine();

  }

  private void PulseRoutine()
  {
    float delta = Mathf.Sin(Time.time * Mathf.PI) / 10f;
    transform.position = transform.position + new Vector3(delta, delta, 0.0f);
  }

}