using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class index : MonoBehaviour
{
    public GameObject SimpleBulletPrefab;
    public GameObject GunAparatus;
    public GameObject FloorGenerationObject;

    public FloorGenerator FloorGeneratorIndex;

    public GameObject Player;

    public static index idx;

    public Dictionary<int, bool> randdict;

    [Header("Rooms")]
    public List<GameObject> normalRooms = new() { };
    public List<GameObject> bossRooms = new() { };
    public List<GameObject> storeRooms = new() { };
    public List<GameObject> itemRooms = new() { };
    public List<GameObject> startRooms = new() { };

    public GameObject randomroom(List<GameObject> rooms)
    {
        return rooms[Random.Range(0, rooms.Count - 1)];
    }

    public GameObject guntoaparatus(GameObject gun, int gunindex, Vector3 position)
    {
        GameObject newap = Instantiate(GunAparatus);

        PlayerAttributes attrs = Player.GetComponent<PlayerAttributes>();

        GunPickupAparatus pickupscript = newap.GetComponent<GunPickupAparatus>();

        gun.transform.parent = newap.transform;

        attrs.playerGuns.RemoveAt(gunindex);

        pickupscript.gun = gun;

        attrs.Gun = null;

        newap.transform.position = position;

        // TODO: make sure that it doesn't clip outside of the world

        return newap;
    }

    public  Vector3 Round(Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    public bool randomBool()
    {
        return randdict[Random.Range(0,2)];
    }

    public float randomSign()
    {

        if (randomBool()) return 1f;
        return -1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        idx = FindObjectOfType<index>();
        FloorGeneratorIndex = FindObjectOfType<FloorGenerator>();

        randdict = new();

        randdict.Add(0, false);
        randdict.Add(1, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
