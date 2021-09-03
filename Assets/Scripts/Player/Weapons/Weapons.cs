using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int slot;

    public Camera playerCam;

    public GameObject revolver;
    public GameObject shotgun;

    public GameObject ui;

    public float amount;
    public float maxAmount;
    public float smoothAmount;

    private Vector3 initPosition;

    public GameObject recoilPlace;
    public int recoilAmount;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;
        slot = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);
        ui.transform.localPosition = Vector3.Lerp(ui.transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);

        if (Input.GetButtonDown("Fire1"))
        {
            transform.position = new Vector3(recoilPlace.transform.position.x,recoilPlace.transform.position.y, recoilPlace.transform.position.z);
            shoot();
        }

        if (Input.GetButtonDown("Gunslot1"))
        {
            // take out revolver
            slot = 1;
        }
        if (Input.GetButtonDown("Gunslot2"))
        {
            // take out shotgun
            slot = 2;
        }

        if (slot == 1)
        {
            revolver.SetActive(true);
            shotgun.SetActive(false);
        }
        else if (slot == 2)
        {
            revolver.SetActive(false);
            shotgun.SetActive(true);
        }


    }

    void shoot()
    {
        RaycastHit hit;
        bool itsHit = Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out hit, 100f);
        bool enemyHit = hit.transform.tag == "Enemy";

        if (enemyHit && slot == 1)
        {
            hit.collider.gameObject.GetComponent<BasicEnemy>().life -= 10;
            print("you have a enemy in front of you");
        }
        else if (enemyHit && slot == 2)
        {
            hit.collider.gameObject.GetComponent<BasicEnemy>().life -= 20;
        }
        else 
        {
            print("that's a wall :b");
        }
    }
}
