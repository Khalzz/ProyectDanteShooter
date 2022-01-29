using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManagement : MonoBehaviour
{
    [SerializeField] // we use this to show on the editor a private variable
    Button startHost;

    [SerializeField]
    Button startClient;

    [SerializeField] 
    Button startServer;

    // Start is called before the first frame update
    void Start()
    {
        startHost.onClick.AddListener(() => 
        {
            if(NetworkManager.Singleton.StartHost())
            {

                print("Host started!!!");
            }
            else 
            {
                print("Host could not be started!!!");
            }
            
        });

        startClient.onClick.AddListener(() => 
        {
            if(NetworkManager.Singleton.StartClient())
            {
                print("Client started!!!");
            }
            else 
            {
                print("Client could not be started!!!");
            }
        });

        startServer.onClick.AddListener(() => 
        {
            if(NetworkManager.Singleton.StartServer())
            {
                print("Server started!!!");
                
            }
            else 
            {
                print("Server could not be started!!!");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
