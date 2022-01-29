/* 
to do: 
    1. Limitate the "weapons" based on what do you get from the floor
    2. Create the "low range attack"
*/

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

    // guns
    public GameObject revolver;
    public GameObject shotgun;
    public int shotGunPellets;

    private GameObject actualGun;
    public int slot;
    public int latestSlot;
    public int latestTempSlot;

    public int equipedWeapons;
    public bool haveRevolver;
    public bool haveShotgun;

    // proyectiles
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
        latestSlot = 2;
        latestTempSlot = 2;

        haveRevolver = false;
        haveShotgun = false;
        equipedWeapons = 0;
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

        if (haveRevolver)
        {
            revolver.SetActive(true);
        }
        else 
        {
            revolver.SetActive(false);
        }

        if (haveShotgun)
        {
            shotgun.SetActive(true);
        }
        else 
        {
            shotgun.SetActive(false);
        }

        // gun slots
        if (Input.GetButtonDown("Gunslot1"))
        {
            Slot1();
        }
        if (Input.GetButtonDown("Gunslot2"))
        {
            Slot2();
        }
        if (Input.GetButtonDown("Quickchange") && equipedWeapons >= 2)
        {
            LatestGun();
        }

        if (slot == 1 && haveRevolver)
        {
            recoilAmount = revolverClass.recoilAmount;
            revolver.SetActive(true);
            shotgun.SetActive(false);
        }
        else if (slot == 2 && haveShotgun)
        {
            recoilAmount = shotgunClass.recoilAmount;
            revolver.SetActive(false);
            shotgun.SetActive(true);
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
            if (slot == 1 && haveRevolver)
            {
                revolverClass.Shoot(playerCam, bulletPrefab, playerBody);
                canShoot = false;
                waitTimer = revolverClass.waitTimer;
                recoilTime = revolverClass.recoilTime;
            }
            else if (slot == 2 && haveShotgun)
            {
                player.transform.parent.GetComponent<Rigidbody>().AddRelativeForce(-playerCam.transform.forward * 1000);
                shotgunClass.Shoot(transform, playerBody, playerCam, bulletPrefab); //Transform weaponContainer, LayerMask playerBody, Camera playerCam, GameObject bulletPrefab
                canShoot = false;
                waitTimer = shotgunClass.waitTimer;
                recoilTime = shotgunClass.recoilTime;
            }
            Vector3 recoilPosition = new Vector3(movementX, movementY, -recoilAmount);
            transform.localPosition = Vector3.Lerp(transform.localPosition, recoilPosition + initPosition, Time.deltaTime * 3);
        }
        // shoot
    }

    public void Slot1() 
    {
        if (slot != 1)
        {
            latestTempSlot = slot;
            latestSlot = slot;

            slot = 1;
        }
    }

    public void Slot2()
    {
        if (slot != 2)
        {
            latestTempSlot = slot;
            latestSlot = slot;

            slot = 2;
        }
    }

    public void LatestGun()
    {
        latestTempSlot = slot;
        slot = latestSlot;
        latestSlot = latestTempSlot;
    }
}
