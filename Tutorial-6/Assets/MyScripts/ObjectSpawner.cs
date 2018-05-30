using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public float timeBetweenSpawns;

    public Stuff[] objectPrefabs;

    float timeSinceLastSpawn;

    public float velocity;

    private void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= timeBetweenSpawns) {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnPrefabs();
        }
    }

    // method to spawn different prefabs based on the selection made in the editor
    void SpawnPrefabs()
    {
        Stuff prefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
        Stuff spawn = Instantiate<Stuff>(prefab);
        spawn.transform.localPosition = transform.position;
        spawn.rigidBody.velocity = transform.up * velocity;
    }
}
