using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupAparatus : MonoBehaviour
{
    public GameObject gun;

    public bool disableNextTrigger;

    public void dropGun(GameObject gun, GameObject player)
    {
        GameObject newAparatus = Instantiate(index.idx.GunAparatus);

        gun.transform.parent = newAparatus.transform;

        newAparatus.transform.position = player.transform.position;

        GunPickupAparatus gpa = newAparatus.GetComponent<GunPickupAparatus>();

        gpa.gun = gun;

        gun.transform.localPosition = new Vector3(0, 0, 0);

        gpa.disableNextTrigger = true;

        gun.GetComponent<BasicGun>().disableShooting = true;

    }

    private void OnTriggerEnter(Collider other) // fdro[t adoijwijoe djoiwhogi
    {
        if (disableNextTrigger)
        {
            disableNextTrigger = false;
            return;
        }

        if (other.CompareTag("Player"))
        {
            //Debug.Log(other.name);
            PlayerAttributes attr = other.GetComponentInParent<PlayerAttributes>();

            try
            {
                if (attr.playerGuns[attr.currentGunIndex].GetComponent<BasicGun>().isReloading)
                {
                    return;
                }
            }
            catch { }

            // Remove gun if maximim guns would be reached
            if (attr.maximumGuns < attr.playerGuns.Count + 1)
            {
                dropGun(attr.Gun, other.gameObject);
                attr.playerGuns.Remove(attr.Gun);
            }

            // we have to call this first before we move it to the child
            attr.playerGuns.Add(gun);
            attr.currentGunIndex = attr.playerGuns.Count - 1;

            gun.transform.parent = attr.GunHolder.transform;

            gun.transform.localPosition = new Vector3(0, 0, 0);
            gun.transform.localRotation = Quaternion.Euler(0, 90, 270);

            attr.disableGunsAndEnableGun(attr.currentGunIndex);

            gun.GetComponent<BasicGun>().disableShooting = false;

            Destroy(this.gameObject);

        }
    }
}
