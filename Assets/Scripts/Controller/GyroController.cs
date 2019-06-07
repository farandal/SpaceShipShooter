using UnityEngine;
using UnityEngine.UI;

public class GyroController : MonoBehaviour
{
  private Gyroscope gyro;
  private bool gyroEnabled;

  void Start()
  {

    gyroEnabled = EnableGyro();

  }

  private bool EnableGyro() {

    if(SystemInfo.supportsGyroscope) {

      gyro = Input.gyro;
      gyro.enabled = true;
      return true;

    }

    return false;

  }

  private void Update()
  {

    if(gyroEnabled) {
      transform.rotation = gyro.attitude;
    }

  }

  //This is a legacy function, check out the UI section for other ways to create your UI
  void OnGUI()
  {
    //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
    GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + gyro.rotationRate);
    GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + gyro.attitude);
    GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + gyro.enabled);
  }
}