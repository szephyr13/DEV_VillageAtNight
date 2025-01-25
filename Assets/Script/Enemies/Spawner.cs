using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [Header("Limits on Spawning")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int enemyLimit;
    [SerializeField] private int enemyCounter;

    private float timer;

    public int EnemyCounter { get => enemyCounter; set => enemyCounter = value; }

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
                Vector3 nextSpawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                GameObject enemyCopy = Instantiate(enemyPrefab, nextSpawnPosition, Quaternion.identity, this.gameObject.transform);
                enemyCounter++;
            }
            timer = 0;
        }

    }
}
