using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    public Mesh fractalMesh;
    public Material newFractalMaterial;

    // the following variables control the amount of frctal children being created
    public int maxDepth;
    private int depth;

	void Start ()
    {
        // adds new mesh and material to the attached gameObject
        gameObject.AddComponent<MeshFilter>().mesh = fractalMesh;
        gameObject.AddComponent<MeshRenderer>().material = newFractalMaterial;
        
        // only allow creation of children if condition satisfies
        if (depth < maxDepth)
        {
            new GameObject("Fractal Child").AddComponent<Fractal>();
        }
	}
}
