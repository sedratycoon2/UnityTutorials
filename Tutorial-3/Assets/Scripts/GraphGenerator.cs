using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;
    // range to create more cubes, which can be adjusted via slider
    [Range(10,100)]
    public int graphResolution = 10;
    Transform [] graphPoints;

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
            newPointPos.y = SineFunction(newPointPos.x, time); // f(x) = sin(pi*x) type function
            newGraphPoint.localPosition = newPointPos;
        }
    }


    float SineFunction(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }
}
