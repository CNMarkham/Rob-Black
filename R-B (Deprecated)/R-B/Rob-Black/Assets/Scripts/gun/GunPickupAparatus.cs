using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupAparatus : MonoBehaviour
{
    public GameObject gun;

    public bool disableNextTrigger;

    public bool nopickup = false;

    public static void dropGun(GameObject gun, GameObject player) // instansiates new gun aparatus with gun input obj as gun
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

    public void doublePickupRadius() // the pickup radius is the bounding bot that the payer needs to touch in order to pick up the gun so this doubles that size
    {
        var box = GetComponent<BoxCollider>();
        box.size = new Vector3(box.size.x * 2, box.size.y * 2, box.size.z * 2);

    }

    public void OnTriggerEnter(Collider other) // Gives palyer gun if it can pick it up
    {
        if (nopickup) return;

        if (disableNextTrigger)
        {
            disableNextTrigger = false;
            return;
        }

        if (other.CompareTag("Player"))
        {
            playercol(other.gameObject);

        }
    }

    public void playercol(GameObject other) // adds gun to player arsenal or replaces current gun if at max
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
