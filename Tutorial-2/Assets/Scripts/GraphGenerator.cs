using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;
    // range to create more cubes, which can be adjusted via slider
    [Range(10,100)]
    public int graphResolution = 10;

    private void Awake()
    {
        float graphStep = 2f / graphResolution; // used to calculate the scale
        Vector3 graphScale = Vector3.one * graphStep;
        Vector3 graphPosition;
        graphPosition.z = 0f;
        for (int i=0; i < graphResolution; i++)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPosition.x = (i + 0.5f) * graphStep - 1f; // sets the x-position of the clone in the range (-1,1)
            graphPosition.y = Mathf.Pow(graphPosition.x, 2); // sets the y-position of the clone in the range (-1,1) 
            graphPoint.localPosition = graphPosition;
            graphPoint.localScale = graphScale; // reducing the scale of the clones to bring to (-1,1) domain
            graphPoint.SetParent(transform, false);
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
