using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;

    private void Awake()
    {
        Vector3 graphScale = Vector3.one / 5f;
        Vector3 graphPosition;
        graphPosition.y = 0;
        graphPosition.z = 0;
        for (int i=0; i < 10; i++)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPosition.x = (i + 0.5f) / 5f - 1f; // sets the position of the clone to (i,0,0) in the range (-1,1)
            graphPoint.localPosition = graphPosition;
            graphPoint.localScale = graphScale; // reducing the scale of the clones to bring to (-1,1) domain
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
