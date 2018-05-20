using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;
    // range to create more cubes, which can be adjusted via slider
    [Range(10,100)]
    public int graphResolution = 10;
    Transform [] graphPoints;
    // creates a slider in the editor to select function
    [Range(0, 1)]
    public int function;
    private void Awake()
    {
        float graphStep = 2f / graphResolution; // used to calculate the scale
        Vector3 graphScale = Vector3.one * graphStep;
        Vector3 graphPosition;
        graphPosition.y = 0f;
        graphPosition.z = 0f;
        graphPoints = new Transform[graphResolution];
        for (int i = 0; i < graphResolution; i++)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPoints[i] = graphPoint; // storing the newly created clone's transform
            graphPosition.x = (i + 0.5f) * graphStep - 1f; // sets the x-position of the clone in the range (-1,1)
            graphPoint.localPosition = graphPosition;
            graphPoint.localScale = graphScale; // reducing the scale of the clones to bring to (-1,1) domain
            graphPoint.SetParent(transform, false);
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }

    private void Update()
    {
        // updates graph point per frame
        float time = Time.time;
        for (int i = 0; i < graphResolution; i++)
        {
            Transform newGraphPoint = graphPoints[i];
            Vector3 newPointPos = newGraphPoint.localPosition;
            // updates graph points based on the type of fuction chosen
            if (function == 0)
                newPointPos.y = SineFunction(newPointPos.x, time);
            else
                newPointPos.y = MultiSineFunction(newPointPos.x, time);
            newGraphPoint.localPosition = newPointPos;
        }
    }

    // method which returns f(y) = sin(pi*(x+t))
    float SineFunction(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    // method which returns f(y) = sin(pi*(x+t)) + (2 * sin(pi*(x+2*t))) / 2
    float MultiSineFunction(float x, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + 2f * t)) / 2f;
        y *= 2f / 3f; // maintains the domain of (-1,1) for f(y)
        return y;
    }
}
