using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask floorLayer, playerLayer;

    public float followRange;
    public float attackRange;

    public bool playerInFollowRange;
    public bool playerInAttackRange;

    // enemy data
    public int life;
    public int attackPower;
    
    public double timer;
    public int fixedTimer;

    public bool haveAttacked;

    private void Awake() 
    {
        haveAttacked = false;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        life = 10;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerInFollowRange = Physics.CheckSphere(transform.position, followRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= 3f && playerInFollowRange)
        {
            agent.isStopped = true;
            timer +=(1 * Time.deltaTime);
            if (timer >= 0.5)
            {
                if (haveAttacked == false)
                {
                    Attacking();
                }
            } 
        }
        else if (dist > 3f && playerInFollowRange)
        {
            followRange = 50;
            Following();
            timer = 0;
        }

        

        if (life <= 0)
        {
            print("ay me disparastes :c");
            Destroy(gameObject);
        }
    }

    public void Shooted()
    {
        print("ay me disparastes :c");
    }

    public void Following()
    {
        agent.isStopped = false;    
        agent.SetDestination(player.position);
    }

    public void Attacking()
    {
        haveAttacked = true;
        Stats.playerLife -= 20;
        haveAttacked = false;
        timer = 0;
    }
}