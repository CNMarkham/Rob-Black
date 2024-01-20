using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class BasicGun : MonoBehaviour
{
    // Add shotgun so ex: bulletsPerShot
    // Add bulletSpread so that the shotgun isn't over powered

    [Header("Gun Data")]

    public string Name;
    public SpriteRenderer spriteRenderer;
    public GameObject Bullet;

    [Header("Gun Settings")]
    public bool holdDown;

    [Header("Bullet Settings")]

    public float bulletSpeed;
    public Transform bulletStartPos;

    [Header("Reloading")]

    public float magazineSize; // Magazine size
    public float reloadTime; // ms // reload time

    [Header("Shooting")]

    public float bulletsPerShot = 1;
    public float bulletsPerSecond;
    public float bulletSpreadDegree;

    [Header("Scattering")]

    // TODO:

    public float scatterFactor;
    public float scatterStart;
    public float scatterEnd;

    [Header("Burst")]

    public bool disableBurst;

    // make it so that the values below get greyed out

    public float burstSize;
    public float timeBetweenBursts;

    [Header("Internal")]

    public int shotsFired;
    public bool canShoot;
    public bool isReloading;


    private void Start()
    {
        canShoot = true;

        if (disableBurst)
        {
            burstSize = 0;
            timeBetweenBursts = 0;
        }


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

        bulletScript.rotateDegrees(Random.Range(-bulletSpreadDegree, bulletSpreadDegree));
        bulletScript.updateRotation();

        bulletScript.enabled = true;
    }

    IEnumerator reload()
    {
        if (isReloading)
        {
            yield break;
        }

        isReloading = true;

        Color32 oldColor = spriteRenderer.color;

        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(((float)(reloadTime)) / 1000f);

        shotsFired = 0;
        canShoot = true;
        isReloading = false;

        spriteRenderer.color = oldColor;
    }

    IEnumerator shoot()
    {
        if (shotsFired >= magazineSize || isReloading || !canShoot)
        {
            canShoot = false;
            yield break;
        }

        canShoot = false;

        for (int i=0; i < bulletsPerShot; i++) 
        {
            instanciateBullet();
        }

        shotsFired += 1;
        yield return new WaitForSeconds((1000f/bulletsPerSecond)/1000f);

        if (shotsFired % burstSize == 0)
        {
            yield return new WaitForSeconds((float)(timeBetweenBursts) / 1000f);
        }

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

        if (

            ((holdDown && Input.GetMouseButton(0)) || (!holdDown && Input.GetMouseButtonDown(0))) && canShoot

            )
        {
            StartCoroutine(shoot());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(reload());
        }
    }
}
