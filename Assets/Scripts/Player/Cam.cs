using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform cam;

    public float sensibilidadMouse; //sensibilidad basica de camara
    public Transform cuerpoJugador; //objeto del jugador
    public Transform playerOrientation; //objeto del jugador
    public float camFov;

    float rotacionX = 0f; //cantidad base de rotacion de camara
    float rotacionY = 0f; //cantidad base de rotacion de camara

    void Start()
    {
        sensibilidadMouse = 200;
        Cursor.lockState = CursorLockMode.Locked; //bloquea el cursor mientras se ejecuta
        camFov = 90;
            
    }

    void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward*100f, Color.magenta);

        if (RbMovement.itsSpeed)
        {
            cam.gameObject.GetComponent<Camera>().fieldOfView = camFov + 20;
        } 
        else 
        {
            cam.gameObject.GetComponent<Camera>().fieldOfView = 90;
        }
        //input de eje x camara
        float camaraX = Input.GetAxisRaw("Mouse X"); 
        //input de eje y camara
        float camaraY = Input.GetAxisRaw("Mouse Y");
        
        rotacionY += camaraX * sensibilidadMouse*0.01f;
        rotacionX -= camaraY * sensibilidadMouse*0.01f;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); //rotacion maxima en eje y

        //rotacion del objeto en base a "rotacionX"
        if (WallRuning.itsRunning == true)
        {
            cam.transform.localRotation = Quaternion.Euler(rotacionX, rotacionY, 2*4*RbMovement.globalX);
        }
        else
        {
            cam.transform.localRotation = Quaternion.Euler(rotacionX, rotacionY, -2*RbMovement.itsMoving);
        }
        playerOrientation.transform.localRotation = Quaternion.Euler(0, rotacionY, 0);
        playerOrientation.Rotate(Vector3.up * camaraX); //rotacion de jugador en base a camara x
    }

    
}
