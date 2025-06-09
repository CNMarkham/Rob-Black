using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class BasicGun : MonoBehaviour
{
    // swords are possible with curreent architecture 

    [Header("Gun Data")]

    public bool disableShooting;
    public string Name;
    public SpriteRenderer spriteRenderer;
    public GameObject Bullet;
    public Color32 bulletBurnColor;

    public float relativeGunWidth = 1f;
    public float relativeGunHeight = 1f;

    public Color32 gunColor = new Color32(255,255,255,255);

    [Header("Gun Settings")]
    public bool holdDown;

    public float viewResizeX = 1;
    public float viewResizeY = 1;

    public float viewOffsetX = 0;
    public float viewOffsetY = 0;

    public float viewRotateZ = 0;

    [Header("Bullet Settings")]

    public float bulletLengthPercentOfStandardBullet = 1; // 1 = it has the same length as the regular bullet prefab; 0.5 half as long, 2 twice as long
    public float bulletHeightPercentOfStandardBullet = 1; // Height instead of length 

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

    public float decayTime;
    public float decayStartDegrees = 0; // KEEP @ 0 FOR MOST OF THE TIME UNLESS YOU WANT A HOLE IN THE MIDDLE
    public float decayEndDegrees;
    public float decayIterations = 100;
    public bool DECAY;

    [Header("Burst")]

    public bool disableBurst;

    public float burstSize;
    public float timeBetweenBursts;

    [Header("Internal")]

    public int shotsFired;
    public bool canShoot;
    public bool isReloading;
    public bool isHeld;

    public bool flashgun;
    public bool flashinggun;


    private void Start()
    {

        canShoot = true;

        if (disableBurst)
        {
            burstSize = 0;
            timeBetweenBursts = 0;
        }

        disableShooting = true;

        spriteRenderer.size = new Vector2(spriteRenderer.size.x * relativeGunWidth, spriteRenderer.size.y * relativeGunHeight);

        bulletDamage = (int)((float)bulletDamage * (index.idx.floornumtogunfloat(PlayerFloorCount.floorNumber)) + 1);

        magazineCount = magazineCount + 20 + mathindex.randomInteger(PlayerFloorCount.floorNumber, PlayerFloorCount.floorNumber * 5);
    }

    IEnumerator flashthegun()
    {
        if (flashinggun) yield break;
        flashinggun = true;

        for (int i = 0; i < 10000; i++)
        {
            if (!flashgun) { break; }

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, i%2);
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);

        flashinggun = false;

    }

    void instanciateBullet() // instansiates bullet
    {
        GameObject newBullet = Instantiate(Bullet);

        newBullet.transform.localScale = new Vector3(bulletLengthPercentOfStandardBullet * 0.2f, bulletHeightPercentOfStandardBullet * 0.2f, (float)0.5);

        SimpleBullet bulletScript = newBullet.GetComponent<SimpleBullet>();

        bulletScript.enabled = false;

        bulletScript.damage = bulletDamage;

        bulletScript.DECAY = DECAY;
        bulletScript.decayTime = decayTime;
        bulletScript.decayStartDegrees = decayStartDegrees;
        bulletScript.decayEndDegrees = decayEndDegrees;
        bulletScript.decayIterations = decayIterations;
        bulletScript.bulletBurnColor = bulletBurnColor;

        bulletScript.rotation = transform.rotation;
        bulletScript.forward = transform.forward;
        bulletScript.right = transform.right;
        newBullet.transform.position = bulletStartPos.position;

        bulletScript.bulletspeed = bulletSpeed;

        bulletScript.rotateDegrees(Random.Range(-bulletSpreadDegree, bulletSpreadDegree));
        bulletScript.updateRotation();

        index.idx.gunaudio.Play();

        bulletScript.enabled = true;
    }

    public IEnumerator reload() // reloads gun when run
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
        flashgun = false;

        spriteRenderer.color = oldColor;
    }

    public IEnumerator shoot() // Shoots gun if magazine is full, not reloading and can shoot is true
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

        if (shotsFired >= magazineSize)
        {
            flashgun = true;
        }

        canShoot = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = gunColor;

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

        if (flashgun)
        {
            StartCoroutine(flashthegun());
        }
    }
}
