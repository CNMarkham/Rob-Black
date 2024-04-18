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

    public Dictionary<int, bool> randdict;

    public bool registered;

    [Header("Rooms")]
    public List<GameObject> normalRooms;
    public List<GameObject> bossRooms;
    public List<GameObject> storeRooms;
    public List<GameObject> itemRooms;
    public List<GameObject> startRooms;

    public GameObject getPlayer()
    {
        return Player;
    }

    public void untilRegistered()
    {
        while (!registered) { }
    }

    public T randomChoice<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public GameObject randomroom(List<GameObject> rooms)
    {
        print(rooms);
        return rooms[Random.Range(0, rooms.Count)];
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
        Debug.Log("randombool");
        return randdict[Random.Range(0,2)];
    }

    public float randomSign()
    {

        if (randomBool()) return 1f;
        return -1f;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("start");
        idx = FindObjectOfType<index>();
        FloorGeneratorIndex = FindObjectOfType<FloorGenerator>();

        randdict = new();

        randdict.Add(0, false);
        randdict.Add(1, true);

        registered = true;
    }

    public static index idx;
}
