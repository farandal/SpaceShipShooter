using UnityEngine;

public static class GameObjectEx
{
    public static void DrawCircle(this GameObject container, float radius, float lineWidth, Color color, Vector3 rotation)
    {

        /* GameObject container = new GameObject();
        container.transform.rotation = Quaternion.Euler(rotation);
        container.transform.SetParent(parent.transform);
        */

        var segments = 360;
        var line = container.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;
        line.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        line.SetColors(color, color);

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius) ;
        }

        line.SetPositions(points);

        /* GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = points[90];
       
        sphere.transform.localScale = new Vector3 (30f,30f, 30f);
        sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        sphere.transform.SetParent(container.transform);
        */

        container.layer = 9;
    
        

    }
}