using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRuning : MonoBehaviour
{
    public Transform orientation;

    public float wallDistance = 0.5f;
    public float minJumpDistance = 1;
    static public bool leftWall = false;
    static public bool rightWall = false;

    static public bool canLeftWallJump;
    RaycastHit leftWallHit;

    static public bool canRightWallJump;
    RaycastHit rightWallHit;

    public float wallRunGravity; 
    public float wallRunJumpForce; 

    public Rigidbody rb;

    static public int timesCanJump;

    static public bool itsRunning;

    public LayerMask world;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpDistance, world);
    }

    public void CheckWall()
    {
        leftWall = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance, world);
        rightWall = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance, world);
    }

    // Update is called once per frame
    void Update()
    {
        CheckWall();
        if (CanWallRun())
        {
            if (leftWall) 
            {
                RbMovement.jumpsLeft = 1;
                StartWallRun();
            }
            else if (rightWall)
            {
                RbMovement.jumpsLeft = 1;
                StartWallRun();

            }
            else 
            {
                itsRunning = false;
                StopWallRun();
            }
        }
        else
        {
            itsRunning = false;
            StopWallRun();
        }
    }
    
    public void StartWallRun()
    {
        itsRunning = true;
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
        if (Input.GetButtonDown("Jump"))
        {
            print("saltaste");

            if (leftWall)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal*5;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if (rightWall)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal*5;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
        }
    }

    public void StopWallRun()
    {
        rb.useGravity = true;
    }
}