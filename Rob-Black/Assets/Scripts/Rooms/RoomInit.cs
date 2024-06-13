using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInit : MonoBehaviour
{
    public List<EnemySpawnModule> spawnLists = new List<EnemySpawnModule>();

    public int enemySpawnNumber;

    public float spawnRate;

    public float spawnRateAmplitude;

    public bool coroutineEnded;

    public List<door> doors;

    private List<GameObject> enemies;

    void doorsAreClosed(bool closed)
    {
        foreach (door d in doors)
        {
            d.isClosed(closed);
        }
    }

    IEnumerator enemySpawnCoroutine() // Spawn an enemy from enemy list at random positkion from enemy positions every spawnrate seconds
    {

        //EnemySpawnModule esm = gameObject.GetComponents<EnemySpawnModule>();
        int currentLevel = PlayerFloorCount.floorNumber;
        if (currentLevel > spawnLists.Count) { currentLevel = spawnLists.Count - 1; }

        for (int i = 0; i < enemySpawnNumber; i++)
        {
            yield return new WaitForSeconds(spawnRate + index.idx.randomSign() * Random.Range(0, spawnRateAmplitude));

            GameObject enemy = index.idx.randomChoice(spawnLists[currentLevel].EnemyTypes);

            Vector3 enemypos = index.idx.randomChoice(spawnLists[currentLevel].EnemySpawns).position;

            GameObject newenemy = Instantiate(enemy, enemypos, Quaternion.identity);

            newenemy.transform.parent = this.transform.parent.transform;

            enemies.Add(newenemy);
        }

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

        StartCoroutine(enemySpawnCoroutine());

        coroutineEnded = true;
    }
}
