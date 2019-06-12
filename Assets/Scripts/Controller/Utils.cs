using System;
using UnityEngine;
public class Utils : MonoBehaviour {

    public static float lerp (float a, float b, float f) {
        return a + f * (b - a);
    }

}