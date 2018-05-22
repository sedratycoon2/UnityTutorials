using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    public Mesh fractalMesh;
    public Material newFractalMaterial;

    // the following two variables control the amount of fractal children being created
    public int maxDepth;
    private int depth;

    public float fractalChildScale;

	void Start ()
    {
        // adds new mesh and material to the attached gameObject
        gameObject.AddComponent<MeshFilter>().mesh = fractalMesh;
        gameObject.AddComponent<MeshRenderer>().material = newFractalMaterial;
        
        // only allow creation of children if condition satisfies
        if (depth < maxDepth)
        {
            new GameObject("Fractal Child").
                AddComponent<Fractal>().InitializeChild(this);
        }
	}

    // method to create new fractal child with similar mesh, material and maxDepth settings as parent
    private void InitializeChild(Fractal parent)
    {
        fractalMesh = parent.fractalMesh;
        newFractalMaterial = parent.newFractalMaterial;
        fractalChildScale = parent.fractalChildScale;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        transform.parent = parent.transform; // sets parent as the root
        transform.localScale = Vector3.one * fractalChildScale; // sets the scale of the child
        // sets the position of the child such that it's placed above the parent
        // while still touching it
        transform.localPosition = Vector3.up * (0.5f + 0.5f * fractalChildScale);
    }
}
