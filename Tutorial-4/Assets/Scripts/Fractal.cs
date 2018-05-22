using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    public Mesh fractalMesh;
    public Material fractalMaterial;
    private Material[] materialPerDepth;

    // variables to control the amount and scale of fractal children being created
    public int maxDepth;
    private int depth;
    public float fractalChildScale;

    // variables to enhance readability of child creation code
    private static Vector3[] fractalChildDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private static Quaternion[] fractalChildOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0, 0, -90f),
        Quaternion.Euler(0, 0, 90f),
        Quaternion.Euler(90f, 0, 0),
        Quaternion.Euler(-90f, 0, 0)
    };

    void Start ()
    {
        if(materialPerDepth == null)
        {
            InitializeMaterialPerDepth();
        }
        // adds new mesh and material to the attached gameObject
        gameObject.AddComponent<MeshFilter>().mesh = fractalMesh;
        gameObject.AddComponent<MeshRenderer>().material = materialPerDepth[depth];

        // only allow creation of children if condition satisfies
        if (depth < maxDepth)
        {
            StartCoroutine(CreateFractalChildren());
        }
	}

    // method to create new fractal child with similar mesh, material and maxDepth settings as parent
    private void InitializeChild (Fractal parent, int childIndex)
    {
        fractalMesh = parent.fractalMesh;
        fractalMaterial = parent.fractalMaterial;
        fractalChildScale = parent.fractalChildScale;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        transform.parent = parent.transform; // sets parent as the root
        transform.localScale = Vector3.one * fractalChildScale; // sets the scale of the child
        // sets the position of the child such that it's placed above the parent while still touching it
        transform.localPosition = fractalChildDirections[childIndex] 
            * (0.5f + 0.5f * fractalChildScale);
        // sets the orientaion of the child to avoid parent-child intersections
        transform.localRotation = fractalChildOrientations[childIndex];
    }

    // coroutine which pauses for 0.5 seconds before creating the child fractal
    private IEnumerator CreateFractalChildren()
    {
        for (int i=0; i < fractalChildDirections.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            new GameObject("Fractal Child").
                    AddComponent<Fractal>().InitializeChild(this, i);
        }
    }

    // method to create only one duplicate material per depth
    private void InitializeMaterialPerDepth()
    {
        materialPerDepth = new Material[maxDepth + 1];
        for(int i=0; i <= maxDepth; i++)
        {
            materialPerDepth[i] = new Material(fractalMaterial);
            materialPerDepth[i].color = 
                Color.Lerp(Color.white, Color.yellow, (float) i/maxDepth);
        }
    }
}
