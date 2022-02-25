using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	private bool spawnedBoss;
	public GameObject boss;

	public float waitOtherRoom;
	public GeneratingLevel loader;

	private void Start()
    {
		loader = GameObject.Find("Spawner").GetComponent<GeneratingLevel>();
	}

    void Update()
    {
        waitOtherRoom += Time.deltaTime;

		if (waitOtherRoom >= 1.5f)
        {
			loader.LoadLevel();
        }

        if (waitTime <= 0 && spawnedBoss == false)
		{
			for (int i = 0; i < rooms.Count; i++)
			{
				if (i == rooms.Count - 1)
				{
					Instantiate(boss, rooms[i].transform.position + new Vector3(0,-8,0), Quaternion.identity);
					spawnedBoss = true;
				}
			}
		}
		else
		{
			waitTime -= Time.deltaTime;
		}
	}
}
