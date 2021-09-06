using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask playerBody;
    public GameObject player;

    static public Vector3 weaponContainer;

    public GameObject bulletPrefab;
    public GameObject floor;

    // guns
    public GameObject revolver;
    public GameObject shotgun;
    public GameObject launcher;
    public int shotGunPellets;

    private GameObject actualGun;
    public int slot;

    // proyectiles
    public GameObject misileProyectile;
    public Transform firePoint;
    private Vector3 destination;
    public float proyectileSpeed;


    // gun recoil
    public int recoilAmount;
    private bool canShoot;
    public float waitTimer;
    public float recoilTime;

    public GameObject ui;

    // ui/guns movement
    public float amount;
    public float maxAmount;
    public float smoothAmount;
    private Vector3 initPosition;

    Shotgun shotgunClass;
    Revolver revolverClass;

    void Start()
    {
        shotgunClass = shotgun.GetComponent<Shotgun>();
        revolverClass = revolver.GetComponent<Revolver>();
        proyectileSpeed = 50;
        canShoot = true;
        actualGun = revolver;
        initPosition = transform.localPosition;
        slot = 1;
    }

    void Update()
    {
        // gun movement by mouse
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);
        ui.transform.localPosition = Vector3.Lerp(ui.transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);
        // gun movement by mouse

        // gun slots
        if (Input.GetButtonDown("Gunslot1"))
        {
            // take out revolver
            actualGun = revolver;
            slot = 1;
        }
        if (Input.GetButtonDown("Gunslot2"))
        {
            slot = 2;
        }
        if (Input.GetButtonDown("Gunslot3"))
        {
            actualGun = launcher;
            slot = 3;
        }

        if (slot == 1)
        {
            recoilAmount = revolverClass.recoilAmount;
            revolver.SetActive(true);
            shotgun.SetActive(false);
            launcher.SetActive(false);
        }
        else if (slot == 2)
        {
            recoilAmount = shotgunClass.recoilAmount;
            revolver.SetActive(false);
            shotgun.SetActive(true);
            launcher.SetActive(false);
        }
        else if (slot == 3)
        {
            recoilAmount = 50;
            revolver.SetActive(false);
            shotgun.SetActive(false);
            launcher.SetActive(true);
        }
        // gun slots

        // recoil wait
        waitTimer +=(1 * Time.deltaTime);
        if (canShoot == false)
        {
            if (waitTimer > recoilTime)
            {
                canShoot = true;
            }
        }
        // recoil wait

        // shoot
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            if (slot == 1)
            {
                revolverClass.Shoot(playerCam, bulletPrefab);
                canShoot = false;
                waitTimer = revolverClass.waitTimer;
                recoilTime = revolverClass.recoilTime;
            }
            else if (slot == 2)
            {
                player.GetComponent<Rigidbody>().AddRelativeForce(-playerCam.transform.forward * 1000);
                shotgunClass.Shoot(transform, playerBody, playerCam, bulletPrefab); //Transform weaponContainer, LayerMask playerBody, Camera playerCam, GameObject bulletPrefab
                shotgunClass.Test();
                canShoot = false;
                waitTimer = shotgunClass.waitTimer;
                recoilTime = shotgunClass.recoilTime;
            }
            /*else if (slot == 3)
            {
                LauncherShoot();
                canShoot = false;
                waitTimer = 0;
                recoilTime = 1f;
            }*/
            Vector3 recoilPosition = new Vector3(movementX, movementY, -recoilAmount);
            transform.localPosition = Vector3.Lerp(transform.localPosition, recoilPosition + initPosition, Time.deltaTime * 3);
        }
        // shoot
    }

    

    void LauncherShoot()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        var projectileObj = Instantiate(misileProyectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * proyectileSpeed;
        Physics.IgnoreCollision(misileProyectile.GetComponent<Collider>(), player.GetComponent<Collider>());
        //projectileObj.GetComponent<Rigidbody>().AddForce(transform.forward * proyectileSpeed);
    }
}
