using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewBehaviourScript : MonoBehaviour {

    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
}
