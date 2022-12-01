using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Spawner[] spawners;
    [SerializeField] int currentSpawner = 0;

    private void Start()
    {
        DisableAll();
        spawners[currentSpawner].enabled = true;
    }

    private void Update()
    {
        if (spawners[currentSpawner].canSpawnBoss && !spawners[currentSpawner].isBossSpawned)
        {
            spawners[currentSpawner].SpawnBoss();
        }
        if (spawners[currentSpawner].finishedSpawning && !spawners[currentSpawner].isFinalSpawner)
        {
            DisableAll();
            currentSpawner++;
            spawners[currentSpawner].enabled = true;
        }
    }

    void DisableAll()
    {
        foreach(var spawner in spawners)
        {
            spawner.enabled = false;
        }
    }
}
