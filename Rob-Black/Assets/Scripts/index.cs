using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

using System;
using System.Linq;

public class index : MonoBehaviour
{
    // Large, possibly useless class that contains many, many, functions

    public GameObject SimpleBulletPrefab;
    public GameObject GunAparatus;
    public GameObject FloorGenerationObject;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public Light2D GlobalLight;

    public GunUIManager guimg;

    public EnvironmentPool environmentPool;

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

    public environment currentenvironment;
    public difficulty currentdifficulty;

    public Environment defaultenvironment;

    public void updatefloor(int floor)
    {
        currentdifficulty = floornumtodiff(floor);
        currentenvironment = environment.test; // CHANGE LATER

        List<Environment> environments = new();

        foreach (Environment potentialenvironment in environmentPool.enviro)
        {
            if (potentialenvironment.environment == currentenvironment)
            {
                environments.Add(potentialenvironment);
            }
        }


        Environment env = defaultenvironment;

        if (environments.Count !=0)
        {
            env = environments[0];
        }

        normalRooms = env.normalRooms;
        bossRooms = env.bossRooms;
        storeRooms = env.storeRooms;
        itemRooms = env.itemRooms;
        startRooms = env.startRooms;

        GlobalLight.color = env.globalLightColour;
        GlobalLight.intensity = env.globalLightIntensity;

        Player.GetComponent<PlayerAttributes>().flashlightEnabled = env.flashlightEnabled;

        defaultenvironment = env;

    }

    public enum difficulty // Speicfy the dificulty of an enemy spawn module
    {
        Easy = 0,
        Medium = 5,
        Hard = 10,
        Extra_Hard = 25,
        Extra_Extra_Hard = 35,

        REQUIEM = 50,

        ARMAGEDDON = 100,

    }

    public enum environment
    {
        test = 0,

        // needs to be implemented in order to complete prototype
        dark = 0xD98, // D = D
                      // 9 = R
                      // 8 = B = Black = K
                      // = DRK

        // 0x means not implemented yet
        cave = 0x1, // tony stark built this in a 0x1!
        sand = 0x2, // sand environment
        arct = 0x3 // arctic
    }

    public List<environment> availableenvironments = new List<environment>() 
        {
    
        environment.test

        };

    public difficulty prevdiff(difficulty difficulty)
    {
        return difficulty switch
        {
           // difficulty.Easy => difficulty.Easy,
            difficulty.Medium => difficulty.Easy,
            difficulty.Hard => difficulty.Medium,
            difficulty.Extra_Hard => difficulty.Hard,
            difficulty.Extra_Extra_Hard => difficulty.Extra_Hard,
            difficulty.REQUIEM => difficulty.Extra_Extra_Hard,
            difficulty.ARMAGEDDON => difficulty.REQUIEM,

            _ => difficulty.Easy
        };
    }

    public difficulty floornumtodiff(int floornum)
    {
        

        // for (int i = Enum.GetNames(typeof(difficulty)).Length - 1; i >= 0; i--)


        // for each dificulty in dificulty enum
        foreach (difficulty difficulty in Enum.GetValues(typeof(difficulty)).Cast<difficulty>().Distinct())
        {
            // if greaterthan or equal to previous dificulty relative to current dificulty return previous dificulty
            if (floornum >= (int)prevdiff(difficulty) && floornum < (int)difficulty) return prevdiff(difficulty);
        }

        // any number greater than 100 or less than 0 defaults to ARMG
        return difficulty.ARMAGEDDON;

    }

    public EnemySpawnModule filterandchoosemodule(List<EnemySpawnModule> modules, difficulty? difficulty = null, environment? environment = null)
    {

        List<EnemySpawnModule> spawnModuleCandidates = new List<EnemySpawnModule>();

        foreach (EnemySpawnModule module in modules)
        {
            // if not wanted dificulty AND dificulty is not set to null and is therefore important then skip this module
            if (module.difficulty != difficulty && difficulty != null)
            {
                continue;
            }

            // same as previous if
            if (module.environment != environment
                && environment != null)
            {
                continue;
            }

            // Add it to candidates
            spawnModuleCandidates.Add(module);
        }

        return randomChoice<EnemySpawnModule>(spawnModuleCandidates);
    }

    public float floornumtodifffloat(int floornum)
    {
        return (float)(0.5f * Mathf.Pow(floornum, 1.5f) + 1f); // math function where f(x) = (x^1.5)0.5 + 1, so f(0) = 1 and f(100) = 501
    }

    public void pay_bills(dollabill.denomonation bill, Transform transform) // spawns bill denomonation @ transforms
    {

        //Debug.LogError(transform);
        Instantiate(bills[(int)bill], transform.position, Quaternion.Euler(new Vector3(90, 0, 0))); // (int)bill
    }

    public void kill_bill() // kills every bill and gun in game
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Money"))
        {
            Destroy(go); // google deepmind????
        }

        foreach (GameObject gun in GameObject.FindGameObjectsWithTag("Gun"))
        {
            /*
            try
            {
                // checks if the player isn't holding the gun
                if (!gun.transform.parent // GunHolder
                    .parent // phishplaceholder
                    .parent // Player
                    .CompareTag("Player")
                )
                {
                    Destroy(gun); // gun
                }
            }
            catch { }
        
            }
            */
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
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public int randomInteger(int x, int y) // import random; return random.randint
    {
        return UnityEngine.Random.Range(x, y);
    }

    public GameObject randomroom(List<GameObject> rooms) // Random Choice but I forgot I had already done it and now I can't change it because it's a dependency
    {
        //print(rooms);
        return rooms[UnityEngine.Random.Range(0, rooms.Count)];
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

    public  Vector3 Round(Vector3 vector3, int decimalPlaces = 2) // round V3 to x decimal points
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
        return randdict[UnityEngine.Random.Range(0,2)];
    }

    public float randomSign() // import random; {True: 1, False: -1}[{1: True, 0: False}[random.randint(0,1)]]
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


        GlobalLight.color = defaultenvironment.globalLightColour;
        GlobalLight.intensity = defaultenvironment.globalLightIntensity;

        Player.GetComponent<PlayerAttributes>().flashlightEnabled = defaultenvironment.flashlightEnabled;

        registered = true;
    }

    int x = 0;

    private void FixedUpdate()
    {
        x += 1;
        print(floornumtodiff(x));
    }

    public static index idx;
}
