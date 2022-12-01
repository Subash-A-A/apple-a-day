using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    class SpawnItem
    {
        public GameObject spawn;
        [Range(0.25f, 1f)]
        public float minSize;
    }

    [SerializeField] Transform[] spawnPoint;
    [SerializeField] SpawnItem[] enemies;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] int maxSpawnAmount = 20;
    [SerializeField] Animator floorAnim;

    [Header("Camera")]
    [SerializeField] float distance = 6f;
    [SerializeField] float yOffset = 0f;
    [SerializeField] CameraFollow follow;

    [Header("Boss")]
    public bool canSpawnBoss = false;
    public bool isBossSpawned = false;
    [SerializeField] GameObject boss;
    [SerializeField] Transform bossSpawn;


    [Header("SpawnerStatus")]
    public bool finishedSpawning = false;
    public bool isFinalSpawner = false;

    private bool canSpawn;
    private int count = 0;

    private void Start()
    {
        canSpawn = true;
        finishedSpawning = false;
    }

    private void Update()
    {
        if (canSpawn && count < maxSpawnAmount)
        {
            StartCoroutine(Spawn());
        }
        else if(count == maxSpawnAmount && !finishedSpawning)
        {
            finishedSpawning = true;
            StartCoroutine(OpenDoor());
        }
        
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, distance, 10 * Time.deltaTime);
        follow.followYOffset = yOffset;
    }

    public void SpawnBoss()
    {
        if (!isBossSpawned)
        {
            Instantiate(boss, bossSpawn.position, Quaternion.identity);
            isBossSpawned = true;
        }
    }

    public IEnumerator Spawn()
    {
        Vector2 spawnPos = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
        canSpawn = false;
        SpawnItem enemyItem = enemies[Random.Range(0, enemies.Length)];
        GameObject enemy = Instantiate(enemyItem.spawn, spawnPos, Quaternion.identity);
        SetEnemyParameters(enemy, enemyItem);
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
        count++;
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
        brain.delayBetweenAttacks *= randomScale;
        brain.attackDamage *= randomScale;
        health.maxHealth *= randomScale;
        anim.SetFloat("chaseSpeed", 1 / randomScale);
    }

    IEnumerator OpenDoor()
    {
        floorAnim.SetBool("Open", true);
        yield return new WaitForSeconds(10f);
        floorAnim.SetBool("Open", false);
    }
}
