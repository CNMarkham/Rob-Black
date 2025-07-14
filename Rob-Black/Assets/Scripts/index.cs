using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

using System;
using System.Linq;

public class index : MonoBehaviour
{
    // Floor Management and Global Variables

    public GameObject SimpleBulletPrefab;
    public GameObject GunAparatus;
    public GameObject FloorGenerationObject;
    public PlayerAttributes playerAttributes;
    public int testint;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public Light2D GlobalLight;

    public GunUIManager guimg;

    public EnvironmentPool environmentPool;

    public FloorGenerator FloorGeneratorIndex;

    public GameObject Player;

    public bool registered;

    public bossuiscript bossuiscript;

    // Stores floor manager which generates and delete rooms and floors
    public floormanager floormanager;

    [Header("gunsprites")]
    public Sprite pistol;
    public Sprite assaultrifle;
    public Sprite russianassaultrifle;

    [Header("Rooms")]
    public List<GameObject> normalRooms;
    public List<GameObject> bossRooms;
    public List<GameObject> storeRooms;
    public List<GameObject> itemRooms;
    public List<GameObject> startRooms;

    public List<GameObject> bills;
    public GameObject heart;

    public environment currentenvironment;
    public difficulty currentdifficulty;

    public Environment defaultenvironment;

    public AudioSource regulargunaudio;
    public AudioSource regulargunaudio2;
    public AudioSource shotgunaudio;
    public AudioSource shotgunaudio2;

    public GameObject reloadtext;

    public void updatefloor(int floor)
    {
        currentdifficulty = floornumtodiff(floor);

        List<Environment> environments = new();

        foreach (Environment potentialenvironment in environmentPool.enviro)
        {
            if (potentialenvironment.environment == currentenvironment)
            {
                environments.Add(potentialenvironment);
            }
        }


        Environment env = mathindex.randomChoice(environments);

        normalRooms = env.normalRooms;
        bossRooms = env.bossRooms;
        storeRooms = env.storeRooms;
        itemRooms = env.itemRooms;
        startRooms = env.startRooms;

        GlobalLight.color = env.globalLightColour;
        GlobalLight.intensity = env.globalLightIntensity;

        Player.GetComponent<PlayerAttributes>().flashlightEnabled = floormanager.setenvironment.flashlightEnabled;

        defaultenvironment = env;

    }

    public enum difficulty // Specify the dificulty of an enemy spawn module
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


        dark = 0xD98, // D = D
                      // 9 = R
                      // 8 = B = Black = K
                      // = DRK
                      // Dark is dark and requires a flashlight
                      // -- modified version of cave/sand

        cave = 0x1, // tony stark built this in a 0x1!
        sand = 0x2, // sand environment
        arct = 0x3 // arctic
    }

    public List<environment> availableenvironments = new List<environment>() 
        {
    
            environment.sand,
            environment.arct,
            environment.cave,
            environment.dark

        };

    public static difficulty prevdiff(difficulty difficulty)
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

        difficulty tep = (difficulty)2;
        // for each difficulty in dificulty enum
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

        return mathindex.randomChoice<EnemySpawnModule>(spawnModuleCandidates);
    }

    public float floornumtodifffloat(int floornum)
    {
        return (float)(0.5f * Mathf.Pow(floornum, 1.5f) + 1f); // math function where f(x) = (x^1.5)0.5 + 1, so f(0) = 1 and f(100) = 501
    }

    public float floornumtogunfloat(int floornum)
    {
        return (float)(0.5f * Mathf.Pow(floornum, 1.1f) + 1f); // math function where f(x) = (x^1.1)0.5 + 1, so f(0) = 1 and f(100) = ~82
    }

    public RangeInt chance_out_of_one_hundred(int x)
    {
        return new RangeInt(1, x);
    }

    public void drop_item(Dictionary<GameObject, RangeInt> items_and_probbabilities, RangeInt probability_range, Transform transform, int items_chosen = 1) // spawns item denomonation @ transforms
    {
        System.Random randomgen = new();

        int diciding_factor = randomgen.Next(probability_range.start, probability_range.end);

        List<GameObject> chosen = new();

        foreach (GameObject i in items_and_probbabilities.Keys)
        {
            // check if item is in the probbability range
            if (items_and_probbabilities[i].start <= diciding_factor && diciding_factor <= items_and_probbabilities[i].end)
            {
                chosen.Add(i);
            }
        }

        List<GameObject> true_chosen = new();

        for (int i = 0; i < items_chosen; i++)
        {
            GameObject go = mathindex.randomChoice(chosen);
            true_chosen.Add(go);
            chosen.Remove(go);
        }

        foreach (GameObject toBeInstantiated in true_chosen)
        {
            if (toBeInstantiated!=null) Instantiate(toBeInstantiated, transform.position, Quaternion.Euler(new Vector3(90, 0, 0))); // (int)bill
        }
    }

    public void kill_bill() // kills every bill in game
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Money"))
        {
            Destroy(go);
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

    public void untilRegistered() // doesn't do anything untill registered is true
    {
        while (!registered) { }
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

    // Start is called before the first frame update
    void Awake()
    {
        idx = FindObjectOfType<index>();
        FloorGeneratorIndex = FindObjectOfType<FloorGenerator>();

        registered = true;
    }

    public static index idx;
}