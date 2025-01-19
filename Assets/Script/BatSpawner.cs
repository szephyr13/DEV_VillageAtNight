using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int batLimit;

    [Header("Limits on Spawning")]
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float upLimit;
    [SerializeField] private float downLimit;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenSpawns)
        {
            GameObject batCopy = Instantiate(batPrefab, new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(upLimit, downLimit), 0), Quaternion.identity);
            timer = 0;
        }
    }
}
