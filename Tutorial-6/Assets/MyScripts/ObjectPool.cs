using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    PooledObject prefab;

    // instantiates prefab and assigns them to a pool
    public PooledObject GetObject()
    {
        PooledObject obj = Instantiate<PooledObject>(prefab);
        obj.transform.SetParent(transform, false);
        obj.Pool = this;
        return obj;
    }

    public void AddObject(PooledObject o)
    {
        Object.Destroy(o.gameObject);
    }
}
