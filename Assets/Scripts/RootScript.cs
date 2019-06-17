using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AppState {
  public static AppState _instance;
  private GameObject mainCameraContainer;
  private GameObject hudCameraContainer;
  private float cameradistance = 800f;
  private float cameraAltitud = 1000f;
  private float playerAltitud = 2400;
  private float terrainScale = 10f;
  private float farClipPlane = 100000f;

  [RuntimeInitializeOnLoadMethod]
  static void OnRuntimeMethodLoad () {
    Instance ();
  }

  public AppState () {
    AppInit ();
  }

  public void InitCameras () {

    // Find the 'main' camera object.
    var original = Camera.main;

    mainCameraContainer = new GameObject {
      name = "MainCameraContainer"
    };
    // Create new cameras to use, copying from the main camera  
    Camera _cam1 = (Camera) Object.Instantiate (
      original,
      new Vector3 (0, 0, 0),
      Quaternion.FromToRotation (new Vector3 (0, 0, 0), new Vector3 (0, 0, 0)));
    _cam1.transform.SetParent (mainCameraContainer.transform, false);

    hudCameraContainer = new GameObject {
      name = "hudCameraContainer"
    };
    Camera _cam2 = (Camera) Object.Instantiate (
      original,
      new Vector3 (0, 0, 0),
      Quaternion.FromToRotation (new Vector3 (0, 0, 0), new Vector3 (0, 0, 1)));
    _cam2.transform.SetParent (hudCameraContainer.transform, false);
    // Set the backgrounds 
    _cam1.backgroundColor = Color.green;
    _cam2.backgroundColor = Color.red;

    // Set render order
    _cam1.depth = 0;
    _cam2.depth = 1;

    // Camera 2 is a minimap, so disable the audio listener and make it small
    var fullScreen = _cam1.pixelRect;
    _cam2.pixelRect = new Rect (fullScreen.xMin, fullScreen.yMin, fullScreen.width * 0.33f, fullScreen.height * 0.33f);

    // Enable cameras!
    _cam1.enabled = true;
    _cam2.enabled = true;

    //Disabel audio listeners
    ((AudioListener) original.GetComponent (typeof (AudioListener))).enabled = false;
    ((AudioListener) _cam2.GetComponent (typeof (AudioListener))).enabled = false;
    //disable main camera
    original.enabled = false;

    _cam1.fieldOfView = 66.0f;
    _cam1.farClipPlane = farClipPlane;

    //_cam1.transform.position = new Vector3 (0f, cameraAltitud, -cameradistance);
    mainCameraContainer.transform.position = new Vector3 (0f, cameraAltitud, -cameradistance);

    _cam2.transform.eulerAngles = new Vector3 (90.0f, _cam2.transform.eulerAngles.y, _cam2.transform.eulerAngles.z);
    _cam2.transform.position = new Vector3 (0f, cameraAltitud - 200, cameradistance / 2);
    _cam2.farClipPlane = farClipPlane;

    // Layers - Unity Editor, Project Settings , Tags & Layers
    // 8 Player
    // 9 Gizmo
    // 10 Terrain
    _cam1.LayerCullingHide ("Gizmo");
    _cam2.LayerCullingHide ("Terrain");
    _cam2.clearFlags = CameraClearFlags.Depth;

  }

  private void SetupIllumination () {
    GameObject lightGameObject = new GameObject ("The Light");
    Light lightComp = lightGameObject.AddComponent<Light> ();
    lightComp.color = Color.white;
    lightGameObject.transform.position = new Vector3 (0, 0, -10);
    //controllerContainer.AddComponent(typeof(GyroController));
  }

  public void AppInit () {

    float scale = 3f;

    this.InitCameras ();
    //this.SetupIllumination ();

    GameObject playerContainer = new GameObject {
      name = "PlayerContainer"
    };

    playerContainer.AddComponent<Plane> ();
    playerContainer.transform.position = new Vector3 (0f, playerAltitud, 0f);
    playerContainer.layer = 8;

    GameObject enemies = new GameObject {
      name = "Enemies"
    };

    DynamicObject enemy = enemies.AddComponent<DynamicObject> ();
    enemy.Setup("enemy","Assets/Resources/shootingBall/shootingBall.obj",new Vector3(-90f,0f,0f),new Vector3(0f,playerAltitud,5000f),ref playerContainer,10);

    GameObject world = new GameObject {
      name = "World"
    };

    World terrain = world.AddComponent<World> ();
    terrain.SetScaleFactor (100f);

    SmoothFollow followCam1 = this.mainCameraContainer.AddComponent<SmoothFollow> () as SmoothFollow;
    SmoothFollow followCam2 = this.hudCameraContainer.AddComponent<SmoothFollow> () as SmoothFollow;

    followCam1.SetTarget (playerContainer);
    followCam2.SetTarget (playerContainer);
    terrain.SetPlayer (playerContainer);

  }

  /* singleton */
  public static AppState Instance () {
    if (_instance == null) {
      _instance = new AppState ();
    }
    return _instance;
  }
}