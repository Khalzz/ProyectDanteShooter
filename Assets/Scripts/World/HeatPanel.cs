using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatPanel : MonoBehaviour
{
    static public bool canBurn;
    public double timer;

    public bool itsOnLava;

    public Vector3 direction;

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

    private void OnCollisionEnter(Collision other) {
        other.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.up*1000f);
        print("funciono :0");
    }
}
