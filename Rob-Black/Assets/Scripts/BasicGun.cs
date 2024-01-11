using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class BasicGun : MonoBehaviour
{

    public GameObject Bullet;

    public float bulletSpeed;
    public Transform bulletStartPos;
    public bool holdDown;

    public float bulletsPerBurst; // Magazine size
    public float timeBetweenBursts; // ms // reload time
    public float bulletsPerSecond; // ms

    public float scatterFactor;
    public float scatterStart;
    public float scatterEnd;

    private int timeinms;
    private bool inBurst;
    private int shotsFired;

    private bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    void instanciateBullet()
    {
        GameObject newBullet = Instantiate(Bullet);

        SimpleBullet bulletScript = newBullet.GetComponent<SimpleBullet>();

        bulletScript.enabled = false;

        bulletScript.scatterFactor = scatterFactor;
        bulletScript.scatterStart = scatterStart;
        bulletScript.scatterEnd = scatterEnd;

        bulletScript.rotation = transform;
        newBullet.transform.position = bulletStartPos.position + new Vector3(-1, 0, 0);

        bulletScript.bulletspeed = bulletSpeed;

        bulletScript.updateRotation();

        bulletScript.enabled = true;
    }

    IEnumerator shoot()
    {
        canShoot = false;
        instanciateBullet();
        yield return new WaitForSeconds((1000f/bulletsPerSecond)/1000f);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // timeinms = (int)Mathf.Floor(Time.time * 1000);

        // time delta time is chjange in time per fram
        // have seperate varialbe always going up (by delta time)
        // each time you wsnty to shoot subtract cooldown from variablde
        // and you can only shoot when the variable is greater than cooldown
        //stop adding delta time if more than cooldown

        // ATUALLTLY USE VARIABLES

        if (holdDown && Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(shoot());
        }

        if (!holdDown && Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(shoot());
        }
    }
}
