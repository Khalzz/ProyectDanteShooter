using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask playerBody;
    public GameObject player;

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
    public GameObject recoilPlace;
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

    void Start()
    {
        proyectileSpeed = 50;
        shotGunPellets = 16;
        waitTimer = 0;
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
            // take out shotgun
            actualGun = shotgun;
            slot = 2;
        }
        if (Input.GetButtonDown("Gunslot3"))
        {
            // take out shotgun
            actualGun = launcher;
            slot = 3;
        }

        if (slot == 1)
        {
            recoilAmount = 20;
            revolver.SetActive(true);
            shotgun.SetActive(false);
            launcher.SetActive(false);
        }
        else if (slot == 2)
        {
            recoilAmount = 100;
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
                RevolverShoot();
                canShoot = false;
                waitTimer = 0;
                recoilTime = 0.2f;
            }
            else if (slot == 2)
            {
                player.GetComponent<Rigidbody>().AddRelativeForce(-playerCam.transform.forward * 1000);
                ShotgunShoot();
                canShoot = false;
                waitTimer = 0;
                recoilTime = 0.6f;
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

    void ShotgunShoot()
    {
        for(int i = 0; i < shotGunPellets; i++)
        {
            float randomAngle = Random.Range(-10, 10);
            Vector3 axis = new Vector3(1,1,0);
            Quaternion rotation = Quaternion.AngleAxis(randomAngle, axis);
            RaycastHit hit;
            bool itsHit = Physics.Raycast(playerCam.transform.position,getShotgunShooting(), out hit, 1000f, ~playerBody);
            bool enemyHit = hit.transform.tag == "Enemy";
            Debug.DrawRay(this.transform.position, rotation*transform.forward*100, Color.magenta);
            
            if (hit.transform.tag == "World")
            {
                Instantiate(bulletPrefab, hit.point, Quaternion.identity);
            }

            if (enemyHit)
            {
                hit.collider.gameObject.GetComponent<BasicEnemy>().life -= 2;
            }
        }
    }

    void RevolverShoot()
    {
        RaycastHit hit;
        bool itsHit = Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out hit, 1000f);
        bool enemyHit = hit.transform.tag == "Enemy";

        if (hit.transform.tag == "World")
        {
            Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }

        if (enemyHit && slot == 1)
        {
            hit.collider.gameObject.GetComponent<BasicEnemy>().life -= 10;
            print("you have a enemy in front of you");
        }
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

    Vector3 getShotgunShooting()
    {
        Vector3 targetPos = playerCam.transform.position + playerCam.transform.forward * 50f;
        targetPos = new Vector3 (
            targetPos.x + Random.Range(-10, 10),
            targetPos.y + Random.Range(-10, 10),
            targetPos.z + Random.Range(-10, 10)
        );
        Vector3 direction = targetPos - playerCam.transform.position;
        return direction.normalized;
    }
}
