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
            graphPoint.localPosition = Vector3.right * i; //sets the position of the clone to (i,0,0)
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
