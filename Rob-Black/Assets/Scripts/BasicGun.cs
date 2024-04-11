using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class BasicGun : MonoBehaviour
{
    // Add shotgun so ex: bulletsPerShot
    // Add bulletSpread so that the shotgun isn't over powered

    // swords are possible with curreent architecture

    [Header("Gun Data")]

    public bool disableShooting;
    public string Name;
    public SpriteRenderer spriteRenderer;
    public GameObject Bullet;
    public Color32 bulletBurnColor;

    public Color32 gunColor = new Color32(255,255,255,255);

    [Header("Gun Settings")]
    public bool holdDown;

    [Header("Bullet Settings")]

    public float bulletSpeed;
    public int bulletDamage;
    public Transform bulletStartPos;

    [Header("Reloading")]

    public int magazineCount;
    public float magazineSize; // Magazine size
    public float reloadTime; // ms // reload time

    [Header("Shooting")]

    public float bulletsPerShot = 1;
    public float bulletsPerSecond;
    public float bulletSpreadDegree;

    [Header("Decay")]

    // TODO:
    // Scattering -- based on decay time

    public float decayTime;
    public float decayStartDegrees = 0; // KEEP @ 0 FOR MOST OF THE TIME UNLESS YOU WANT A HOLE IN THE MIDDLE
    public float decayEndDegrees;
    public float decayIterations = 100;
    public bool DECAY;

    [Header("Burst")]

    public bool disableBurst;

    // make it so that the values below get greyed out

    public float burstSize;
    public float timeBetweenBursts;

    [Header("Internal")]

    public int shotsFired;
    public bool canShoot;
    public bool isReloading;
    public bool isHeld;


    private void Start()
    {
        canShoot = true;

        if (disableBurst)
        {
            burstSize = 0;
            timeBetweenBursts = 0;
        }

        disableShooting = true;
    }

    void instanciateBullet()
    {
        GameObject newBullet = Instantiate(Bullet);

        SimpleBullet bulletScript = newBullet.GetComponent<SimpleBullet>();

        bulletScript.enabled = false;

        bulletScript.DECAY = DECAY;
        bulletScript.decayTime = decayTime;
        bulletScript.decayStartDegrees = decayStartDegrees;
        bulletScript.decayEndDegrees = decayEndDegrees;
        bulletScript.decayIterations = decayIterations;
        bulletScript.bulletBurnColor = bulletBurnColor;

        bulletScript.rotation = transform.rotation;
        bulletScript.forward = transform.forward;
        bulletScript.right = transform.right;
        newBullet.transform.position = bulletStartPos.position + new Vector3(-1, 0, 0);

        bulletScript.bulletspeed = bulletSpeed;

        bulletScript.rotateDegrees(Random.Range(-bulletSpreadDegree, bulletSpreadDegree));
        bulletScript.updateRotation();

        bulletScript.enabled = true;
    }

    IEnumerator reload()
    {
        if (isReloading || magazineCount <= 0 || shotsFired == 0)
        {
            yield break;
        }

        magazineCount -= 1;

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

        spriteRenderer.color = gunColor; // maybe add 2 colors and have them occilate between another

        if (disableShooting) return;

        if (

            ((holdDown && Input.GetMouseButton(0)) || (!holdDown && Input.GetMouseButtonDown(0))) && canShoot && isHeld

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
