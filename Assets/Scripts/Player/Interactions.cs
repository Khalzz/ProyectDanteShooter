using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask playerBody;
    public Transform lastSelected;
    public GameObject pickableSelector;
    public Weapons weapons;
    public Transform playerHand;
    public RaycastHit pickedObject;

    private void Start()
    {
        weapons = GetComponentInChildren<Weapons>();
    }

    void Update()
    {
        RaycastHit hit;
        bool itsHit = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 10f, ~playerBody);
        lastSelected = hit.transform;
        if (lastSelected != null && lastSelected.tag == "Pickable" && itsHit && pickedObject.collider == null)
        {
            pickableSelector.transform.position = hit.transform.position;
            pickableSelector.transform.SetParent(hit.transform);
            pickableSelector.SetActive(true);
            if (Input.GetButtonDown("Fire1") && weapons.slot == 0)
            {
                pickedObject = hit;
                pickedObject.transform.gameObject.layer = 7; // guns layer
                pickedObject.transform.Rotate(-90, 0, 0);
            }
        }
        else
        {
            pickableSelector.SetActive(false);
        }
        if (pickedObject.collider != null)
        {
            pickedObject.transform.position = playerHand.position;
        }
        // the drop object is needed
    }
}
