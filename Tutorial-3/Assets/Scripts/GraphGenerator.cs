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
    static GraphFunction [] functions = { SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction, Ripple};
    private void Awake()
    {
        float graphStep = 2f / graphResolution; // used to calculate the scale
        Vector3 graphScale = Vector3.one * graphStep;
        Vector3 graphPosition;
        graphPosition.y = 0f;
        graphPosition.z = 0f;
        graphPoints = new Transform[graphResolution * graphResolution];
        for (int i = 0; i < graphPoints.Length; i++)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPosition.x = (x + 0.5f) * graphStep - 1f; // sets the x-position of the clone in the range (-1,1)
            graphPoint.localPosition = graphPosition;
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

    // method which returns f(x,z,t) = sin(pi*(x+t))
    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    // method which returns f(x,z,t) = sin(pi*(x+t)) + (2 * sin(pi*(x+2*t))) / 2
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

    // method which returns f(x,z,t) = (sin(pi * (x+t)) + sin(pi * (z+t))) /2
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

    // method which returns f(x,z,t) = 4M + Sx + (Sz/2);
    // where M = sin(pi * (x + z + 0.5t)), Sx = sin(pi * (x+t))
    // and Sz = sin(2 * pi * (z + 2t))
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

    // method which returns f(x,z,t) = (sin(pi * (4d-t))) / (10d + 1);
    // where d = sqrt(x^2 + z^2)
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
}
