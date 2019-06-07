using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    
  #region Terrain
  Chunk[, ] worldChunks;
  public int worldSize = 10;
  public int chunkSize = 30;
  public int chunkHeight = 50;
  public float surfaceCrossValue = 0;
  public float noiseScaleFactor = 20;
  public Vector3 startPosition = new Vector3 (0, 0, 0);
  public Material material;

  #endregion

    // Start is called before the first frame update
    void Start()
    {
        material = Resources.Load("Materials/TerrainTexture", typeof(Material)) as Material;     
        SetupTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
  private void SetupTerrain () {

    Vector3 cornerPosition = startPosition - new Vector3 (worldSize * chunkSize / 2, 0, worldSize * chunkSize / 2);

    worldChunks = new Chunk[worldSize + 1, worldSize + 1];

    for (int i = 0; i < worldSize; i++) {
      for (int j = 0; j < worldSize; j++) {
        worldChunks[i, j] = new Chunk (cornerPosition.x + i * chunkSize, 0, cornerPosition.z + j * chunkSize, chunkSize, chunkHeight, surfaceCrossValue, noiseScaleFactor, material, gameObject);
      }
    }

  }
}
