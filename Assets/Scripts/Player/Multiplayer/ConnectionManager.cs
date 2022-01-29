using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ConnectionManager : MonoBehaviour
{
    public bool server;
    public bool client;
    public bool host;

    // Start is called before the first frame update
    void Start()
    {
        server = false;
        client = false;
        host = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
