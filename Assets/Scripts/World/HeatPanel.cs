using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatPanel : MonoBehaviour
{
    public bool itsOnLava;

    public Vector3 direction;

    private void OnCollisionEnter(Collision other) {
        Stats.GettingDamage(40);
        other.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.up*1000f);
        print("funciono :0");
    }
}
