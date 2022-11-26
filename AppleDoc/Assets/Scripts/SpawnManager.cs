using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    class SpawnItem
    {
        public GameObject spawn;
        [Range(0.25f, 1f)]
        public float minSize;
    }

    [SerializeField] SpawnItem[] enemies;
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

        if(spawnPos.y < 0)
        {
            spawnPos.y = 0;
        }

        canSpawn = false;
        SpawnItem enemyItem = enemies[Random.Range(0, enemies.Length)];
        GameObject enemy = Instantiate(enemyItem.spawn, spawnPos, Quaternion.identity);

        SetEnemyParameters(enemy, enemyItem);

        count++;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

    private void SetEnemyParameters(GameObject enemy, SpawnItem enemyItem)
    {
        EnemyBrain brain = enemy.GetComponent<EnemyBrain>();
        Animator anim = enemy.GetComponent<Animator>();
        Health health = enemy.GetComponent<Health>();
        float randomScale = Random.Range(enemyItem.minSize, 1f);
        enemy.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        brain.movementSpeed /= randomScale;
        brain.jumpForce /= randomScale;
        health.maxHealth *= randomScale;
        anim.SetFloat("chaseSpeed", 1 / randomScale);
    }
    
}
