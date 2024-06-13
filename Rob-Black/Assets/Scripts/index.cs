using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class index : MonoBehaviour
{
    // Large, possibly useless class that contains many, many, functions

    public GameObject SimpleBulletPrefab;
    public GameObject GunAparatus;
    public GameObject FloorGenerationObject;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public GunUIManager guimg;

    public FloorGenerator FloorGeneratorIndex;

    public GameObject Player;

    public Dictionary<int, bool> randdict;

    public bool registered;

    // Stores floor manager which generates and delete rooms and floors
    public floormanager floormanager;

    [Header("Rooms")]
    public List<GameObject> normalRooms;
    public List<GameObject> bossRooms;
    public List<GameObject> storeRooms;
    public List<GameObject> itemRooms;
    public List<GameObject> startRooms;

    public List<GameObject> bills;

    public enum difficulty // Speicfy the dificulty of an enemy spawn module
    {
        Easy = 2,
        Medium = 5,
        Hard = 10,
        Extra_Hard = 25,
        Extra_Extra_Hard = 35,

        REQUIEM = 50,

        ARMAGEDDON = 100,
      

    }

    public void pay_bills(dollabill.denomonation bill, Transform transform) // spawns bill denomonation @ transforms
    {

        //Debug.LogError(transform);
        Instantiate(bills[(int)bill], transform.position, Quaternion.Euler(new Vector3(90, 0, 0))); // (int)bill
    }

    public void kill_bill() // kills every bill in game
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Money"))
        {
            Destroy(go); // google deepmind????
        }

    }
    
    public void screenblack(bool isblack) // makes screen black
    {
        guimg.setblack(isblack);
    }

    public GameObject getPlayer() // Returns player (depricated)
    {
        return Player;
    }

    public void untilRegistered() // doesn't do anything untill registered is true (DO NOT USE THIS 😭)
    {
        while (!registered) { }
    }

    public T randomChoice<T>(List<T> list) // import random; return random.choice
    {
        return list[Random.Range(0, list.Count)];
    }

    public int randomInteger(int x, int y) // import random; return random.randint
    {
        return Random.Range(x, y);
    }

    public GameObject randomroom(List<GameObject> rooms) // Random Choice but I forgot I had already done it
    {
        //print(rooms);
        return rooms[Random.Range(0, rooms.Count)];
    }

    public GameObject guntoaparatus(GameObject gun, int gunindex, Vector3 position) // "Drops" player gun at index gunindex at position position
    {
        GameObject newap = Instantiate(GunAparatus);

        PlayerAttributes attrs = Player.GetComponent<PlayerAttributes>();

        GunPickupAparatus pickupscript = newap.GetComponent<GunPickupAparatus>();

        gun.transform.parent = newap.transform;

        attrs.playerGuns.RemoveAt(gunindex);

        pickupscript.gun = gun;

        attrs.Gun = null;

        newap.transform.position = position;

        return newap;
    }

    public  Vector3 Round(Vector3 vector3, int decimalPlaces = 2) // weird function I don't use or understand
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

    public bool randomBool() // import random; {1: True, 0: False}[random.randint(0,1)]
    {
        //Debug.Log("randombool");
        return randdict[Random.Range(0,2)];
    }

    public float randomSign() // import random; {1: 1, 0: -1}[random.randint(0,1)]
    {

        if (randomBool()) return 1f;
        return -1f;
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("start");
        idx = FindObjectOfType<index>();
        FloorGeneratorIndex = FindObjectOfType<FloorGenerator>();

        randdict = new();

        randdict.Add(0, false);
        randdict.Add(1, true);

        registered = true;
    }

    public static index idx;
}
