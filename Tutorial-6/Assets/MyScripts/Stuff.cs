using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Stuff : MonoBehaviour {

    public Rigidbody rigidBody { get; private set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
}
