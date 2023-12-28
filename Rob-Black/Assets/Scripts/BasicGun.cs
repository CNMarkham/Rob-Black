using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{

    public GameObject Bullet;

    public float bulletSpeed;
    public Transform bulletStartPos;
    public bool holdDown;
    public int bulletsPerBurst;
    public int timeBetweenBursts; // ms
    public int timeBetweenBullets; // ms

    public float scatterFactor;
    public float scatterStart;
    public float scatterEnd;

    private int timeinms;
    private bool inBurst;
    private int shotsFired;

    // Start is called before the first frame update
    void Start()
    {

    }

    void shoot()
    {
        if (inBurst && timeinms % timeBetweenBullets == 0)
        {

            shotsFired += 1;

            GameObject newBullet = Instantiate(Bullet);

            SimpleBullet bulletScript = newBullet.GetComponent<SimpleBullet>();

            bulletScript.enabled = false;

            bulletScript.scatterFactor = scatterFactor;
            bulletScript.scatterStart = scatterStart;
            bulletScript.scatterEnd = scatterEnd;

            bulletScript.rotation = transform;
            newBullet.transform.position = bulletStartPos.position + new Vector3(bulletStartPos.rotation.eulerAngles.x,0,0);

            bulletScript.bulletspeed = bulletSpeed;

            bulletScript.updateRotation();

            bulletScript.enabled = true;
        }

        if (shotsFired >= bulletsPerBurst)
        {
            inBurst = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeinms = (int)Mathf.Floor(Time.time * 1000);

        if (timeinms % timeBetweenBursts == 0)
        {
            inBurst = true;
        }

        if (holdDown && Input.GetMouseButton(0))
        {
            shoot();
        }

        if (!holdDown && Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }
}
