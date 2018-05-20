using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;

    private void Awake()
    {
        Vector3 graphScale = Vector3.one / 5f;
        Vector3 graphPosition;
        graphPosition.z = 0;
        for (int i=0; i < 10; i++)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPosition.x = (i + 0.5f) / 5f - 1f; // sets the x-position of the clone in the range (-1,1)
            graphPosition.y = Mathf.Pow(graphPosition.x, 2); // sets the y-position of the clone in the range (-1,1) 
            graphPoint.localPosition = graphPosition;
            graphPoint.localScale = graphScale; // reducing the scale of the clones to bring to (-1,1) domain
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
