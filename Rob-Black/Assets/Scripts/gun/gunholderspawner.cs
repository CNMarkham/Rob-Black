using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunholderspawner : MonoBehaviour
{
    public List<GameObject> gunlist;

    public GameObject gun;
    public GameObject player;

    public int price = 0;
    public TMPro.TMP_Text priceTag;

    public TMPro.TMP_Text toBuy;

    public GunPickupAparatus gpa;
    public PlayerMoney pa;

    public int priceRangeStart;
    public int priceRangeEnd;

    public bool disableAfterFirstFloor;

    public bool nopickup;

    // Start is called before the first frame update
    void Start()
    {
        price = Random.Range(priceRangeStart, priceRangeEnd);

        gun = Instantiate(index.idx.randomChoice(gunlist));

        GameObject newap = Instantiate(index.idx.GunAparatus);
        gpa = newap.GetComponent<GunPickupAparatus>();
        gpa.gun = gun;
        newap.transform.parent = transform;
        gun.transform.parent = newap.transform;

        gun.transform.localPosition = new Vector3(0, 0.5f, 0);
        gun.transform.Rotate(0, 100, 0);

        newap.transform.position = transform.position + new Vector3(0, 0, 0);

        if (price > 0) { nopickup = true; }

        pa = index.idx.Player.GetComponent<PlayerMoney>();
    } // Set price, text & gun

    private void Update()
    { // makes gun  free w/o a specified price

        if (disableAfterFirstFloor && PlayerFloorCount.floorNumber > 1) { Destroy(this.transform.parent.transform.parent.gameObject); }

        if (gpa == null) return;

        gpa.nopickup = nopickup;

        if (price == 0)
        {
            priceTag.text = "";
            toBuy.text = "";
        }

        else
        {
            priceTag.text = price.ToString();
        }
    }

    private void OnTriggerEnter(Collider other) // makes the "to buy press _" text appear when in proximity
    {
        if (gpa == null) return;
        toBuy.text = "To buy press 'E'";
        pa.tobepurchased = gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        toBuy.text = "";
        pa.tobepurchased = null;
    }
}
