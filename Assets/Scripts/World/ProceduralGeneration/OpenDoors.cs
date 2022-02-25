using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class OpenDoors : MonoBehaviour
{
    private NavMeshSurface surface;

    public float WaitTime;
    private int openDoors = 0;
    private int createNavMesh = 0;

    [SerializeField] GameObject doors;
    public List<GameObject> rooms;


    public GeneratingLevel loader;

    private RoomTemplates templates;


    private void Start()
    {
        doors.SetActive(false);
        templates = GameObject.Find("RoomTemplates").GetComponent<RoomTemplates>();
        loader = GameObject.Find("Spawner").GetComponent<GeneratingLevel>();
        //surface = GameObject.Find("Floor").GetComponent<NavMeshSurface>();
    }

    void Update()
    {
        rooms = templates.rooms;
        if (loader.isLoaded && createNavMesh == 0)
        {
            //templates.rooms[0].transform.GetChild(0).GetComponent<NavMeshSurface>().BuildNavMesh();
            createNavMesh += 1;
            doors.SetActive(true);
        }
        if (openDoors == 0 && loader.counter >= 3f)
        {
            doors.GetComponent<Animation>().Play("OpenDoors");
            openDoors += 1;
        }
    }
}
