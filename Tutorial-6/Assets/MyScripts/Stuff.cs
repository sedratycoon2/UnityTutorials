using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject {

    public Rigidbody rigidBody { get; private set; }

    MeshRenderer[] meshRenderers;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>(); // to add meshRenderer to custom prefabs
    }

    private void OnTriggerEnter(Collider enteredCollider)
    {
        if (enteredCollider.CompareTag("Kill Zone"))
        {
            ReturnToPool();
        }
    }

    // method to be used by spawned prefab to set materials to all prefabs especially for custom made
    public void SetMaterial(Material mat)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = mat;
        }
    }
}
