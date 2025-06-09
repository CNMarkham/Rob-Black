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

    IEnumerator enemySpawnCoroutine() // Spawn an enemy from enemy list at random positkion from enemy positions every spawnrate seconds
    {

        RoomFunction.lockWholeRoom = true;

        print(spawnLists[0].environment);

        EnemySpawnModule esm = index.idx.filterandchoosemodule(spawnLists, null, index.idx.floormanager.setenvironment.environment); // (spawnlists, ..., ...)
        // acctually implement current env stuff

        int currentLevel = PlayerFloorCount.floorNumber;
        if (currentLevel > spawnLists.Count) { currentLevel = spawnLists.Count - 1; }

        for (int i = 0; i < Mathf.Max(enemySpawnNumber - (currentLevel==1 ? 5 : 0), 1); i++)
        {
            yield return new WaitForSeconds(spawnRate + mathindex.randomSign() * Random.Range(0, spawnRateAmplitude));

            GameObject enemy = mathindex.randomChoice(esm.EnemyTypes);

            Vector3 enemypos = mathindex.randomChoice(this.enemySpawns).position; // esm.EnemySpawns

            GameObject newenemy = Instantiate(enemy, enemypos, Quaternion.identity);

            newenemy.transform.parent = this.transform.parent.transform;

            enemies.Add(newenemy);
        }

        RoomFunction.roomStarted = true;

        StartCoroutine(waitforenemiestodie());


    }

    IEnumerator waitforenemiestodie()  // waits until end of frame repeatedly untill all enemies are dead
    {
        while (enemies.Count > 0) { yield return new WaitForEndOfFrame(); }
    }

    public void initializeRoom() // starts enemy spawn corutine when run, unless the init function has already been run
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
