using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;

    private void Awake()
    {
        Instantiate(graphPointPrefab); // creates a clone of the cube
        Destroy(graphPointPrefab); // destroys the original gameObject
    }
}
