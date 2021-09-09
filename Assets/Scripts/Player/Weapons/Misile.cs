using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile : MonoBehaviour
{
    private bool collided;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player" && other.gameObject.tag != "Bullet" &&  !collided && other.gameObject.tag != "Enemy")
        {
            collided = true;
            Stats.playerLife -= 25;
        }
        else if (other.gameObject.tag == "World")
        {
            Destroy(gameObject);
        }
    }
}
