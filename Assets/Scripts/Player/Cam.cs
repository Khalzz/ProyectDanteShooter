using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float sensibilidadMouse = 100f; //sensibilidad basica de camara
    public Transform cuerpoJugador; //objeto del jugador
    public float camFov;

    float rotacionX = 0f; //cantidad base de rotacion de camara

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //bloquea el cursor mientras se ejecuta
        camFov = 90;
            
    }

    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward*100f, Color.magenta);

        if (Movement.itsSpeed)
        {
            this.gameObject.GetComponent<Camera>().fieldOfView = camFov + 20;
        } 
        else 
        {
            this.gameObject.GetComponent<Camera>().fieldOfView = 90;
        }
        //input de eje x camara
        float camaraX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime; 
        //input de eje y camara
        float camaraY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;
        
        rotacionX -= camaraY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); //rotacion maxima en eje y

        //rotacion del objeto en base a "rotacionX"
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, -2*Movement.itsMoving);
        cuerpoJugador.Rotate(Vector3.up * camaraX); //rotacion de jugador en base a camara x
    }

    
}
