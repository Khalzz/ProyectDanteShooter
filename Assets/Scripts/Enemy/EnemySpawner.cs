using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool canSpawn;
    private bool haveSpawned = false;
    [SerializeField] GameObject[] enemys;

    public GeneratingLevel loader;

    void Start()
    {
        loader = GameObject.Find("Spawner").GetComponent<GeneratingLevel>();
        canSpawn = randBool;
    }

    void Update()
    {
        if (canSpawn && !haveSpawned && loader.counter >= 2f)
        {
            Instantiate(enemys[Random.Range(0, enemys.Length)], transform.position, transform.rotation);
            haveSpawned = true;
        }
    }

    bool randBool
    {
        get { return (Random.value > 0.5f); }
    }
}
