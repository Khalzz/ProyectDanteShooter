using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool canSpawn;
    private bool haveSpawned = false;
    private bool enemySpawned = false;
    [SerializeField] GameObject[] enemys;

    public GeneratingLevel loader;

    void Start()
    {
        loader = GameObject.Find("Spawner").GetComponent<GeneratingLevel>();
        canSpawn = randBool;
    }

    void Update()
    {
        if (canSpawn && !haveSpawned && !enemySpawned)
        {
            Instantiate(enemys[Random.Range(0, enemys.Length)], transform.position, transform.rotation);
            haveSpawned = true;
            enemySpawned = true;
        }
    }

    bool randBool
    {
        get { return (Random.value > 0.5f); }
    }
}
