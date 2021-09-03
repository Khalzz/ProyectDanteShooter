using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller; //llamamos el componente Character controller

    public GameObject guns;

    static public float itsMoving;

    float speed; //definimos la velocidad como nula (recomiendo 3 para caminar lento)

    //recordar que esto va en el mismo codigo que nuestro archivo 
    public float gravedad; //variable gravedad (-9.81f base)(-39.24 recomendado)
    Vector3 velocidad; //vector de velocidad

    public Transform crouchPosition; // crouch object
    public Transform posicionPies; //objeto pies

    public float radioPies = 0.4f; //radio de comprobacion pies

    public LayerMask Suelo; //layer que represente el suelo
    bool estaEnElSuelo; //esta tocando el suelo?
    float airMult = 0.4f;
    bool itsCrouching;

    static public bool itsSpeed;

    //Recordar que este codigo va en el mismo archivo que el de fisicas y movimiento
    //altura de salto (se puede editar)
    public float alturaSalto;
    void Start() 
    {
        speed = 7;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal"); //input horizontal (teclado y joystick)
        float z = Input.GetAxis("Vertical"); //input vertical (teclado y joystick)

        Vector3 move = transform.right * x + transform.forward * z; // creamos el movimiento

        controller.Move(move * speed * Time.deltaTime);	//ejecutamos el movimiento

        //esta tocando el suelo?
        estaEnElSuelo = Physics.CheckSphere(posicionPies.position, radioPies, Suelo);
        itsCrouching = Physics.CheckSphere(crouchPosition.position, radioPies, Suelo);

        if (estaEnElSuelo)
        {
            controller.Move(move * speed * Time.deltaTime);	//ejecutamos el movimiento
        }
        else if (!estaEnElSuelo)
        {
            controller.Move(move * (speed * airMult) * Time.deltaTime);	//ejecutamos el movimiento
        }

        //si esta en el suelo y la velocidad es menor a 0
        if(estaEnElSuelo && velocidad.y < 0)
        {
            //la velocidad del eje y sera -2f
            velocidad.y = -2f;
        }
        
        itsMoving = x;

        //ajustar valor de velocidad.y
        velocidad.y += gravedad * Time.deltaTime;
        //ejecutamos la "gravedad" cuando sea necesario
        controller.Move(velocidad * Time.deltaTime);

        //si se presiona el boton saltar y se esta en el suelo
        if (Input.GetButton("Jump") && estaEnElSuelo || Input.GetButton("Jump") && itsCrouching)
        {
            //calculo que genera el salto
            velocidad.y = Mathf.Sqrt(alturaSalto * -2 * gravedad);
        }

        if (Input.GetButton("Fire2"))
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (Input.GetButton("Slide")) 
        {
            if (itsMoving != 0)
            {
                itsMoving = x*4;
            }
            if (x != 0 || z != 0)
            {
                itsSpeed = true;
            }
            controller.height = 0f;
            controller.center = new Vector3(0, 0.5f, 0);
            controller.Move(move * 10 * Time.deltaTime);
        }
        else if (Input.GetButtonUp("Slide")) 
        {
            itsSpeed = false;
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            controller.height = 2.0f;
            controller.center = new Vector3(0, 0, 0);
            //velocidad.y = Mathf.Sqrt(1 * -2 * -50);
        }
    }
}
