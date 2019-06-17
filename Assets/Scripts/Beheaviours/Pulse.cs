using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{

  private float factor = 0.1f;

  public void Setup(float factor) {
      this.factor = factor;
  }

  void Start()
  {

  }

  void Update()
  {

    PulseRoutine();

  }

  private void PulseRoutine()
  {
    float delta = Mathf.Sin(Time.time * Mathf.PI) * factor;
    transform.position = transform.position + new Vector3(delta, delta, 0.0f);
  }

}