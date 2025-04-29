using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomInit : MonoBehaviour
{

    public List<EnemySpawnModule> spawnLists;
    public List<Transform> enemySpawns;

    public RoomFunction RoomFunction;

    public int enemySpawnNumber;

    public float spawnRate;

    public float spawnRateAmplitude;

    public bool coroutineEnded;

    public List<door> doors;

    private List<GameObject> enemies;

    private void Start()
    {

        if (spawnLists == null)
        {
            spawnLists = new List<EnemySpawnModule>(GetComponentsInChildren<EnemySpawnModule>());
        }
    }

    void doorsAreClosed(bool closed)
    {
        foreach (door d in doors)
        {
            
        }
    }

    IEnumerator enemySpawnCoroutine() // Spawn an enemy from enemy list at random positkion from enemy positions every spawnrate seconds
    {

        RoomFunction.lockWholeRoom = true;

        print(spawnLists[0].environment);

        EnemySpawnModule esm = index.idx.filterandchoosemodule(spawnLists, null, index.idx.floormanager.setenvironment.environment); // (spawnlists, ..., ...)
        // acctually implement current env stuff

        //EnemySpawnModule esm = gameObject.GetComponents<EnemySpawnModule>();
        int currentLevel = PlayerFloorCount.floorNumber;
        if (currentLevel > spawnLists.Count) { currentLevel = spawnLists.Count - 1; }

        for (int i = 0; i < Mathf.Max(enemySpawnNumber - (currentLevel==1 ? 5 : 0), 1); i++)
        {
            yield return new WaitForSeconds(spawnRate + index.idx.randomSign() * Random.Range(0, spawnRateAmplitude));

            GameObject enemy = index.idx.randomChoice(esm.EnemyTypes);

            Vector3 enemypos = index.idx.randomChoice(this.enemySpawns).position; // esm.EnemySpawns

            GameObject newenemy = Instantiate(enemy, enemypos, Quaternion.identity);

            newenemy.transform.parent = this.transform.parent.transform;

            enemies.Add(newenemy);
        }

        RoomFunction.roomStarted = true;

        StartCoroutine(waitforenemiestodie());


    }

    IEnumerator waitforenemiestodie()  // waits untill end of frame repeatedly untill all enemies are dead
    {
        while (enemies.Count > 0) { yield return new WaitForEndOfFrame(); }
        //opendoors();
    }

    public void initializeRoom() // starts enemyt spawn corutine when run, unless the init function has alreafdy been run
    {

        if (coroutineEnded)
        {
            return;
        }

        enemies = new List<GameObject>() { };

        // close doors
        // add door close function

        if (!RoomFunction.nonattackingroom) {
            StartCoroutine(enemySpawnCoroutine());

            coroutineEnded = true;
        }

    }
}
