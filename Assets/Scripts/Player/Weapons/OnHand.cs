using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHand : MonoBehaviour
{
    public GameObject hand;

    // Update is called once per frame
    void Update()
    {
        transform.parent = hand.transform;
    }
}
