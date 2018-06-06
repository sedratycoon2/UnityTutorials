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
    
    public static ObjectPool GetPool(PooledObject prefab)
    {
        GameObject obj;
        ObjectPool pool;
        if (Application.isEditor) {
            obj = GameObject.Find(prefab.name + " Pool");
            if (obj) {
                pool = obj.GetComponent<ObjectPool>();
                if (pool) {
                    return pool;
                }
            }
        }
        obj = new GameObject(prefab.name + " Pool");
        pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }
}
