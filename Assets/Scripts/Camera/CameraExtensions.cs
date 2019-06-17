using UnityEngine;

/*

Author:  Charles Cordingley - charles at jarrahtechnology dot com - 2014

To change the culling mask programmatically some bitfield maths is required. 
Each layer is represented by a bit in an integer. 
Layer 12 is 000000000000000000100000000000 - the twelfth bit in a 32-bit integer mask. 
The culling mask itself is just the combination (bitwise OR) of these layers. 
Thus, if the culling mask is 000000000010000000100000001000 (equivalent to the number 526344), 
the 4th, 12th and 20th layers would be displayed, as the 4th, 12th and 20th bits are turned on. 
To do this in code, first find the number of the layer and then shift a bit this number of places,
then add or remove this from the mask. 
For example, to add the "Enemy" layer to the mask would be: 
    cam.cullingMask |= 1 << LayerMask.NameToLayer("Enemy"). 
Other bitwise operations can remove layers.


    // 1 << 0 = 1      Binary: 0000000001
    // 1 << 1 = 2      Binary: 0000000010
    // 1 << 2 = 4      Binary: 0000000100
    // 1 << 3 = 8      Binary: 0000001000
    // 1 << 4 = 16     Binary: 0000010000
    // 1 << 5 = 32     Binary: 0000100000
    // 1 << 6 = 64     Binary: 0001000000
    // 1 << 7 = 128    Binary: 0010000000
    // 1 << 8 = 256    Binary: 0100000000
    // 1 << 9 = 512    Binary: 1000000000

*/
public static class CameraExtensions {
    public static void LayerCullingShow(this Camera cam, int layerMask) {
        cam.cullingMask |= layerMask;
    }
    public static void LayerCullingShow(this Camera cam, string layer) {
        LayerCullingShow(cam, 1 << LayerMask.NameToLayer(layer));
    }
    public static void LayerCullingHide(this Camera cam, int layerMask) {
        cam.cullingMask &= ~layerMask;
    }
    public static void LayerCullingHide(this Camera cam, string layer) {
        LayerCullingHide(cam, 1 << LayerMask.NameToLayer(layer));
    }
    public static void LayerCullingToggle(this Camera cam, int layerMask) {
        cam.cullingMask ^= layerMask;
    }
    public static void LayerCullingToggle(this Camera cam, string layer) {
        LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer));
    }
    public static bool LayerCullingIncludes(this Camera cam, int layerMask) {
        return (cam.cullingMask & layerMask) > 0;
    }
    public static bool LayerCullingIncludes(this Camera cam, string layer) {
        return LayerCullingIncludes(cam, 1 << LayerMask.NameToLayer(layer));
    }
    public static void LayerCullingToggle(this Camera cam, int layerMask, bool isOn) {
        bool included = LayerCullingIncludes(cam, layerMask);
        if (isOn && !included) {
            LayerCullingShow(cam, layerMask);
        } else if (!isOn && included) {
            LayerCullingHide(cam, layerMask);
        }
    }
    public static void LayerCullingToggle(this Camera cam, string layer, bool isOn) {
        LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer), isOn);
    }
}