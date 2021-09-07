using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMovement : MonoBehaviour
{
    public Transform posicionPies;
    public Transform crouchPosition;
    public Transform playerOrientation;
    public float radioPies; //radio de comprobacion pies
    public LayerMask suelo;

    static public float itsMoving;
    static public bool itsSpeed;

    public float speed = 10f;

    float x;
    float y;

    Vector3 move;
    Rigidbody rb;

    public bool isGrounded;
    public bool itsCrouching;
    public bool pressingCrouch;
    
    public bool canJump;
    public int jumpForce;

    public float groundDrag = 3f;
    public float crouchDrag = 3f;
    public float airDrag = 1f;


    // Start is called before the first frame update
    void Start()
    {
        radioPies = 0.2f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(posicionPies.position, radioPies, suelo);
        itsCrouching = Physics.CheckSphere(crouchPosition.position, radioPies, suelo);
        Debug.DrawRay(transform.position, -transform.up, Color.magenta);

        float x = Input.GetAxis("Horizontal"); //input horizontal (teclado y joystick)
        float z = Input.GetAxis("Vertical"); //input vertical (teclado y joystick)
        itsMoving = x;

        move = playerOrientation.right * x + playerOrientation.forward * z; // creamos el movimiento

        if (Input.GetButton("Slide")) 
        {
            pressingCrouch = true;
            if (itsMoving != 0)
            {
                itsMoving = x*4;
            }
            if (x != 0 || z != 0)
            {
                itsSpeed = true;
            }
            GetComponent<CapsuleCollider>().height = 0.5f;
            GetComponent<CapsuleCollider>().center = new Vector3(0,0.5f,0);
        }
        else if (Input.GetButtonUp("Slide"))
        {
            pressingCrouch = false;
            itsSpeed = false;
            itsMoving = x;
            GetComponent<CapsuleCollider>().height = 2f;
            GetComponent<CapsuleCollider>().center = new Vector3(0,0,0);

            // i have to do this because the player was clipping into the wall and falling out... fuck u unity
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        }

        if (isGrounded || itsCrouching)
        {
            canJump = true;
            rb.drag = groundDrag;
        }
        else
        {
            canJump = false;
            rb.drag = airDrag;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            //calculo que genera el salto
            //velocidad.y = Mathf.Sqrt(alturaSalto * -2 * gravedad);
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void FixedUpdate() // we move rigidbody here for the physics
    {
        
        if (isGrounded)
        {
            //print(rb.velocity.magnitude);
            rb.AddForce(move.normalized * speed*9.5f, ForceMode.Acceleration);
        }
        else if (!isGrounded && !itsCrouching && !pressingCrouch)
        {
            rb.AddForce(move.normalized * speed*12f*0.4f, ForceMode.Acceleration);
        }
        else if (!isGrounded && !itsCrouching && pressingCrouch)
        {
            rb.AddForce(move.normalized * speed*12f*0.8f, ForceMode.Acceleration);
        }
        else if (itsCrouching && itsSpeed)
        {
            //print(rb.velocity.magnitude);
            rb.AddForce(move.normalized * (speed*17f), ForceMode.Acceleration);
        }
    }
}