using UnityEngine;

public class PooledObject : MonoBehaviour {

    public ObjectPool Pool { get; set; }

    // adds objects to pool and destroys it if no pool is assigned to it
    public void ReturnToPool()
    {
        if (Pool) {
            Pool.AddObject(this);
        }
        else {
            Destroy(gameObject);
        }
    }
}
