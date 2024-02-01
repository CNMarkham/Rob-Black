using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupAparatus : MonoBehaviour
{
    public GameObject gun;

    private void OnTriggerEnter(Collider other) // fdro[t adoijwijoe djoiwhogi
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttributes attr = other.GetComponentInParent<PlayerAttributes>();

            GameObject droppedGun = null;
            var destroy = true;

            // Remove gun if maximim guns would be reached
            if (attr.maximumGuns < attr.playerGuns.Count + 1)
            {
                attr.playerGuns.RemoveAt(attr.currentGunIndex);
                attr.Gun.transform.parent = this.gameObject.transform;
                droppedGun = attr.Gun;
                droppedGun.GetComponent<BasicGun>().disableShooting = true;
                attr.Gun = null;
                destroy = false;
            }

            // we have to call this first before we move it to the child
            attr.playerGuns.Add(gun);
            attr.currentGunIndex = attr.playerGuns.Count - 1;

            gun.transform.parent = other.transform.GetChild(0).transform;

            gun.transform.localPosition = new Vector3(0, 0, 0);
            gun.transform.localRotation = Quaternion.Euler(0, 90, 270);

            attr.disableGunsAndEnableGun(attr.currentGunIndex);

            if (destroy)
            {
                Destroy(this.gameObject);
            }

            else
            {
                gun = droppedGun;
                droppedGun.transform.localPosition = new Vector3(0, 0, 0);
            }

        }
    }
}
