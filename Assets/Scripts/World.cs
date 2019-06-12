using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    #region Terrain
    Chunk[, ] worldChunks;
    public int worldSize = 5;
    public int chunkSize = 50;
    //Hieger than 50, proportion is not  squared.
    public int chunkHeight = 50;
    public float surfaceCrossValue = 0;
    public float noiseScaleFactor = 20;
    public Vector3 startPosition = new Vector3 (0, 0, 0);
    public Material material;

    private GameObject player;
    public void SetPlayer (GameObject player) {
        this.player = player;
    }

    private Vector3 scaleFactorVector;
    private float scaleFactor;
    public void SetScaleFactor (float factor) {
        this.scaleFactor = factor;
        this.scaleFactorVector = new Vector3 (factor, factor, factor);
    }
    #endregion
    // Start is called before the first frame update
    void Start () {
        material = Resources.Load ("Materials/TerrainTexture", typeof (Material)) as Material;
        gameObject.transform.localScale = this.scaleFactorVector;
        gameObject.layer = 10;
        SetupTerrain ();
        //Gizmo gizmo = gameObject.AddComponent<Gizmo> ();
        //gizmo.SetTarget (gameObject);
    }

    // Update is called once per frame
    void Update () {
        StopCoroutine ("ProcessTerrainChunkUpdates");
        StartCoroutine ("ProcessTerrainChunkUpdates");
    }

    IEnumerator ProcessTerrainChunkUpdates () {

        if ( (player.transform.position.z/this.scaleFactor) > startPosition.z + chunkSize) {
            startPosition += new Vector3 (0, 0, chunkSize);
            Vector3 cornerPosition = startPosition - new Vector3 (worldSize * chunkSize / 2, 0, worldSize * chunkSize / 2);
            for (int i = 0; i < worldSize; i++) {
                for (int j = 0; j < worldSize; j++) {
                    if (j == 0) {
                        worldChunks[i, j].Dispose ();
                    } else if (j != worldSize - 1) {
                        worldChunks[i, j] = worldChunks[i, j - 1];
                    } else {
                        worldChunks[i, j] = new Chunk (cornerPosition.x + i * chunkSize, 0, cornerPosition.z + j * chunkSize, chunkSize, chunkHeight, surfaceCrossValue, noiseScaleFactor, material, gameObject);
                    }
                }
            }
        }

        if ( (player.transform.position.z/this.scaleFactor) < startPosition.z - chunkSize) {
            startPosition -= new Vector3 (0, 0, chunkSize);
            Vector3 cornerPosition = startPosition - new Vector3 (worldSize * chunkSize / 2, 0, worldSize * chunkSize / 2);
            for (int i = 0; i < worldSize; i++) {
                for (int j = worldSize - 1; j >= 0; j--) {
                    if (j == worldSize - 1) {
                        worldChunks[i, j].Dispose ();
                    } else if (j != 0) {
                        worldChunks[i, j] = worldChunks[i, j + 1];
                    } else {
                        worldChunks[i, j] = new Chunk (cornerPosition.x + i * chunkSize, 0, cornerPosition.z + j * chunkSize, chunkSize, chunkHeight, surfaceCrossValue, noiseScaleFactor, material, gameObject);
                    }
                }
            }
        }

        yield return null;

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
