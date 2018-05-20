using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour {

    public Transform graphPointPrefab;

    private void Awake()
    {
        int i = 0;
        while (i < 10)
        {
            Transform graphPoint = Instantiate(graphPointPrefab); // creates a clone of the cube
            graphPoint.localPosition = Vector3.right * i; //sets the position of the clone to (i,0,0)
            i = i + 1;
        }
        Destroy(GameObject.Find("Cube")); // destroys the original gameObject
    }
}
