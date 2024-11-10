using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithGun : MonoBehaviour
{
    public BasicGun gun;
    public int shootdelayms;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {

        while (true)
        {

            yield return StartCoroutine(gun.shoot());
            yield return new WaitForSeconds(shootdelayms / 1000);

            if (gun.shotsFired <= gun.magazineSize)
            {
                yield return StartCoroutine(gun.reload());
            }
        }
    }
}