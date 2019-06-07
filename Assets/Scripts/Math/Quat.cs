using UnityEngine;
public class Quat {
    // Represents w + xi + yj + zk
    public float w, x, y, z;
    public Quat(float w, float x, float y, float z){
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }
 
    public float Norm(){
        return Mathf.Sqrt (w * w + x * x + y * y + z * z);
    }
 
    public Quat Normalize(){
        float m = Norm ();
        return new Quat (w / m, x / m, y / m, z / m);
    }
 
    // Returns a*b
    public static Quat Multiply(Quat a, Quat b){
        float w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z;
        float x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y;
        float y = a.w * b.y + a.y * b.w - a.x * b.z + a.z * b.x;
        float z = a.w * b.z + a.z * b.w + a.x * b.y - a.y * b.x;
        return new Quat (w,x,y,z).Normalize();
    }
 
    public Quaternion ToUnityQuaternion(){
        return new Quaternion (w, x, y, z);
    }
}