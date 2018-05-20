using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;

    private void Awake()
    {
        for (int i=0; i < 10; i++)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPoint.localPosition = Vector3.right * (i + 0.5f / 5f - 1f); // sets the position of the clone to (i,0,0) in the range [-1,1]
            graphPoint.localScale = Vector3.one / 5f; // reducing the scale of the clones to bring to [-1,1] domain
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
