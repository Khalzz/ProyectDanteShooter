using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatPanel : MonoBehaviour
{
    static public bool canBurn;
    public double timer;

    public bool itsOnLava;

    // Start is called before the first frame update
    void Start()
    {
        itsOnLava = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (itsOnLava) 
        {
            RbMovement.onLava = true;
        }
        else 
        {
            RbMovement.onLava = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        GetComponent<Rigidbody>().AddExplosionForce(10f, transform.position, 5f, 3f);
        itsOnLava = true;
        if (canBurn)
        {
            Stats.GettingDamage(30);
            canBurn = false;
            timer = 0;
        }
        if (!canBurn)
            timer +=(1 * Time.deltaTime);
            if (timer >= 1) 
            {
                canBurn = true;
            }  
    }

    private void OnTriggerExit(Collider other) {
        itsOnLava = false;
    }
}
