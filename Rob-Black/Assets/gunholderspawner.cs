using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunholderspawner : MonoBehaviour
{
    public List<GameObject> gunlist;

    public GameObject gun;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gun = Instantiate(index.idx.randomChoice(gunlist));

        GameObject newap = Instantiate(index.idx.GunAparatus);
        newap.GetComponent<GunPickupAparatus>().gun = gun;
        newap.transform.parent = transform;
        gun.transform.parent = newap.transform;

        gun.transform.localPosition = new Vector3(0, 0.5f, 0);
        gun.transform.Rotate(0, 100, 0);

        newap.transform.position = transform.position + new Vector3(0, 0, 0);
    }
}
