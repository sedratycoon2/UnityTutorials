using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    const float pi = Mathf.PI;
    public Transform graphPointPrefab;

    // range to create more cubes, which can be adjusted via slider
    [Range(10,100)]
    public int graphResolution = 10;
    Transform [] graphPoints;

    // creates a drop-down list to select function
    public GraphFunctionName functionName;

    // array of delegates
    static GraphFunction [] functions = { SineFunction, Sine2DFunction, MultiSineFunction,
        MultiSine2DFunction, Ripple, Cylinder, Sphere};
    private void Awake()
    {
        float graphStep = 2f / graphResolution; // used to calculate the scale
        Vector3 graphScale = Vector3.one * graphStep;
        graphPoints = new Transform[graphResolution * graphResolution];
        for (int i = 0; i < graphPoints.Length; i++)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPoint.localScale = graphScale; // reducing the scale of the clones to bring to (-1,1) domain
            graphPoint.SetParent(transform, false);
            graphPoints[i] = graphPoint; // storing the newly created clone's transform
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }

    private void Update()
    {
        // updates graph point per frame
        float time = Time.time;
        GraphFunction graphFunction = functions[(int)functionName]; // uses the above created delegate array
        float graphStep = 2f / graphResolution;
        for (int i = 0, z = 0; z < graphResolution; z++)
        {
            float v = (z + 0.5f) * graphStep - 1f;
            for (int x = 0; x < graphResolution; x++, i++)
            {
                float u = (x + 0.5f) * graphStep - 1f;
                graphPoints[i].localPosition = graphFunction(u,v,time);
            }
        }
    }

    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f; // maintains the domain of (-1,1)
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 Ripple(float x, float z, float t)
    {
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z);
        p.x = x;
        p.y = Mathf.Sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;
        float radius = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x = radius *Mathf.Sin(pi * u);
        p.y = v;
        p.z = radius * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float radius = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        radius += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = radius * Mathf.Cos(pi * 0.5f * v);
        p.x = s * Mathf.Sin(pi * u);
        p.y = radius * Mathf.Sin(pi * 0.5f * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }
}
