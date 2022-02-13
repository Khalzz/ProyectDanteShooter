using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int recoilAmount;
    public float waitTimer;
    public float recoilTime;
    public Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        waitTimer = 0;
        recoilTime = 0.2f;
        recoilAmount = 50;
    }

    public void Punch(Camera playerCam, GameObject bulletPrefab, LayerMask playerBody)
    {
        RaycastHit hit;
        RaycastHit lHit;

        bool itsHit = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 0f); //, ~playerBody); // 1.5f
        bool largeHit = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out lHit, 1000f);

        bool worldHit = hit.transform.tag == "World";
        bool enemyHit = hit.transform.tag == "Enemy";
        if (hit.transform.tag == "World")
        {
            Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
        if (enemyHit)
        { 
            if (hit.collider.gameObject.GetComponent<BasicEnemy>() != null)
            {
                hit.collider.gameObject.GetComponent<BasicEnemy>().life -= 50;
            }
            else if (hit.collider.gameObject.GetComponent<BasicEnemyDistance>() != null)
            {
                hit.collider.gameObject.GetComponent<BasicEnemyDistance>().life -= 50;
            }
            else if (hit.collider.gameObject.GetComponent<TestingRangeEnemy>() != null)
            {
                hit.collider.gameObject.GetComponent<TestingRangeEnemy>().life -= 50;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<BasicEnemy>() != null)
            {
                other.gameObject.GetComponent<BasicEnemy>().life -= 50;
            }
            else if (other.gameObject.GetComponent<BasicEnemyDistance>() != null)
            {
                other.gameObject.GetComponent<BasicEnemyDistance>().life -= 50;
            }
            else if (other.gameObject.GetComponent<TestingRangeEnemy>() != null)
            {
                other.gameObject.GetComponent<TestingRangeEnemy>().life -= 50;
            }
        }
    }
}
