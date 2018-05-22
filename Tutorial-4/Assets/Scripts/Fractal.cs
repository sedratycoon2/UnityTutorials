using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    public Mesh fractalMesh;
    public Material newFractalMaterial;

	void Start ()
    {
        // adds new mesh and material to the attached gameObject
        gameObject.AddComponent<MeshFilter>().mesh = fractalMesh;
        gameObject.AddComponent<MeshRenderer>().material = newFractalMaterial;
	}
}
