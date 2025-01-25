using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;

    [Header("Limits on Spawning")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int enemyLimit;
    [SerializeField] private int enemyCounter;

    private ObjectPool<Enemy> enemyPool;

    private float timer;

    public int EnemyCounter { get => enemyCounter; set => enemyCounter = value; }

    private void Start()
    {
        enemyPool = new ObjectPool<Enemy>(CreateE, null, ReleaseE, null);
    }


    void Update()
    {
        timer += Time.deltaTime;
        EnemySpawning();
    }

    private void EnemySpawning()
    {
        if (timer > timeBetweenSpawns)
        {
            if (enemyCounter < enemyLimit)
            {
                Enemy enemyCopy = enemyPool.Get();
                enemyCounter++;
            }
            timer = 0;
        }

    }

    private Enemy CreateE()
    {
        Vector3 nextSpawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        Enemy enemyCopy = Instantiate(enemyPrefab, nextSpawnPosition, Quaternion.identity, this.gameObject.transform);
        enemyCopy.MyPool = enemyPool;
        return enemyCopy;
    }

    private void ReleaseE(Enemy obj)
    {
        obj.gameObject.SetActive(false);
    }

}
