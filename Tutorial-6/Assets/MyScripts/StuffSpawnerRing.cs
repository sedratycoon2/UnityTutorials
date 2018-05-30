using UnityEngine;

public class StuffSpawnerRing : MonoBehaviour {

    public int numberOfPrefabSpawners;

    public float radius, tiltAngle;

    public ObjectSpawner spawnerPrefab;

    private void Awake()
    {
        for (int i = 0; i < numberOfPrefabSpawners; i++)
        {
            CreateSpawner(i);
        }
    }

    // method to create rotater object per spawn to handle placement and roations in isolation
    void CreateSpawner(int index)
    {
        Transform rotater = new GameObject("Rotater").transform;
        rotater.SetParent(transform, false);
        rotater.localRotation = Quaternion.Euler(0f, index * 360f / numberOfPrefabSpawners, 0f);

        ObjectSpawner spawner = Instantiate <ObjectSpawner>(spawnerPrefab);
        spawner.transform.SetParent(rotater, false);
        spawner.transform.localPosition = new Vector3(0f, 0f, radius);
        spawner.transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
    }
}
