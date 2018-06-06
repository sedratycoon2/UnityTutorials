using UnityEngine;

public class PooledObject : MonoBehaviour {

    public ObjectPool Pool { get; set; }

    [System.NonSerialized]
    ObjectPool poolInstanceForPrefab;

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

    public T GetPooledInstance<T> () where T : PooledObject
    {
        return (T) poolInstanceForPrefab.GetObject();
    }
}
