using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] int maxSpawnAmount = 20;
    [SerializeField] float spawnRadius = 10f;

    private bool canSpawn;
    private int count;

    private void Start()
    {
        canSpawn = true;
    }

    private void Update()
    {
        if (canSpawn && count < maxSpawnAmount)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {   
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;
        print(spawnPos);

        if(spawnPos.y < 0)
        {
            spawnPos.y = 0;
        }

        canSpawn = false;
        Instantiate(enemies[Random.Range(0, enemies.Length - 1)], spawnPos, Quaternion.identity);
        count++;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
    
}
