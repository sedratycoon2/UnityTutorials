using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;

    float currentSpawnDelay;

    public Stuff[] objectPrefabs;

    float timeSinceLastSpawn;

    public float velocity;

    public Material prefabMaterial;

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
        Stuff spawn = prefab.GetPooledInstance<Stuff>();
        spawn.SetMaterial(prefabMaterial);

        spawn.transform.localPosition = transform.position;
        spawn.transform.localScale = Vector3.one * scale.RandomInRange;
        spawn.transform.localRotation = Random.rotation;

        spawn.rigidBody.velocity = transform.up * velocity + 
            Random.onUnitSphere * randomVelocity.RandomInRange;
        spawn.rigidBody.angularVelocity = Random.onUnitSphere * 
            angularVelocity.RandomInRange;
    }
}
