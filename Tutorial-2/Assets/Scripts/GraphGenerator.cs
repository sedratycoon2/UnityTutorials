using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;

    private void Awake()
    {
        Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
        graphPoint.localPosition = Vector3.right; //sets the position of the clone to (1,0,0)

        graphPoint = Instantiate(graphPointPrefab); // creates another clone of cube
        graphPoint.localPosition = Vector3.right * 2f; // sets the position of the clone to (2,0,0)
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
