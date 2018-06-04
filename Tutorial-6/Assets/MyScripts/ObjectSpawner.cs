using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public FloatRange timeBetweenSpawns, scale;

    float currentSpawnDelay;

    public Stuff[] objectPrefabs;

    float timeSinceLastSpawn;

    public float velocity;

    private void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= currentSpawnDelay) {
            timeSinceLastSpawn -= currentSpawnDelay;
            currentSpawnDelay = timeBetweenSpawns.RandomInRange;
            SpawnPrefabs();
        }
    }

    // method to spawn different prefabs based on the selection made in the editor
    void SpawnPrefabs()
    {
        Stuff prefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
        Stuff spawn = Instantiate<Stuff>(prefab);
        spawn.transform.localPosition = transform.position;
        spawn.transform.localScale = Vector3.one * scale.RandomInRange;
        spawn.transform.localRotation = Random.rotation;
        spawn.rigidBody.velocity = transform.up * velocity;
    }
}
