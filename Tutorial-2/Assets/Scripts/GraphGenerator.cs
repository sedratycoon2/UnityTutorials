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
            graphPoint.localPosition = Vector3.right * i / 5f; // sets the position of the clone to (i,0,0) w.r.t the current scale
            graphPoint.localScale = Vector3.one / 5f; // reducing the scale of the clones to 1/5th of the original
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
