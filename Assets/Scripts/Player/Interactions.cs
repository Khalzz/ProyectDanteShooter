using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask playerBody;
    public Transform lastSelected;
    public GameObject pickableSelector;

    void Update()
    {
        RaycastHit hit;
        bool itsHit = Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out hit, 10f, ~playerBody);
        lastSelected = hit.transform;
        if (lastSelected != null && lastSelected.tag == "Pickable" && itsHit)
        {
            pickableSelector.transform.position = hit.transform.position;
            pickableSelector.transform.SetParent(hit.transform);
            pickableSelector.SetActive(true);
            // aqui agregamos que ocurre cuando interactuas con estos objetos
        } 
        else 
        {
            pickableSelector.SetActive(false);
        }
    }
}
