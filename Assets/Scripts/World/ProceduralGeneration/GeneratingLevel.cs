using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingLevel : MonoBehaviour
{
    private RoomTemplates templates;

    public GameObject player;
    public GameObject loadingCamera;

    public GameObject[] Enemy;

    public GameObject gameMenu;
    public GameObject loadMenu;
    public GameObject pressM1;

    public bool canSpawnEnemys = false;
    public bool isLoaded = false;
    public bool isSpawned = false;

    public bool startCounter = false;
    public float counter;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        loadingCamera.SetActive(true);
        gameMenu.SetActive(false);
        loadMenu.SetActive(true);
        pressM1.SetActive(false);
    }

    void Update()
    {
        if (isLoaded && Input.GetButtonDown("Fire1") && !isSpawned)
        {
            startLevel();
        }

        if (startCounter == true)
        {
            counter += Time.deltaTime;
        }
    }

    public void LoadLevel()
    {
        isLoaded = true;
        pressM1.SetActive(true);
    }

    private void startLevel()
    {
        isSpawned = true;
        gameMenu.SetActive(true);
        loadMenu.SetActive(false);
        loadingCamera.SetActive(false);
        Instantiate(player, transform.position, transform.rotation);
        startCounter = true;
    }
}
