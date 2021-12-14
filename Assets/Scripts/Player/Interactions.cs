using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask playerBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool itsHit = Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out hit, 1000f, ~playerBody);
        
        if (Input.GetButtonDown("Fire1") && hit.transform.tag == "FinishButton") {}
    }
}
