using UnityEngine;
using System.Collections;
 
public class PerlinNoiseTexture
{
  // Use this for initialization

  // Create a texture and fill it with Perlin noise.
  // Try varying the xOrg, yOrg and scale values in the inspector
  // while in Play mode to see the effect they have on the noise.

  // Width and height of the texture in pixels.
  private int pixWidth = 360;
  private int pixHeight = 360;

  // The origin of the sampled area in the plane.
  private float xOrg = 0.0f;
  private float yOrg = 0.0f;

  // The number of cycles of the basic noise pattern that are repeated
  // over the width and height of the texture.
  private float scale = 1.0f;

  Texture2D noiseTex;
  Color[] pix;

  public Texture2D GenerateTexture() {

    noiseTex = new Texture2D(pixWidth, pixHeight, TextureFormat.RGB24, false);
    pix = new Color[noiseTex.width * noiseTex.height];
    CalcNoise();

    return noiseTex;

  }

  void CalcNoise()
  {
    // For each pixel in the texture...
    for (int y = 0; y < noiseTex.height; y++)
    {
      for (int x = 0; x < noiseTex.width; x++)
      {
        // Get a sample from the corresponding position in the noise plane
        // and create a greyscale pixel from it.
        float xCoord = xOrg + x / noiseTex.width * scale;
        float yCoord = yOrg + y / noiseTex.height * scale;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        pix[y * noiseTex.width + x] = new Color(sample, sample, sample);
      }
    }
    // Copy the pixel data to the texture and load it into the GPU.
    noiseTex.SetPixels(pix);
    noiseTex.SetPixel(50, 50, Color.white);
    noiseTex.SetPixel(310, 310, Color.white);
    noiseTex.Apply();
  }
}
