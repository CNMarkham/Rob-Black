using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInit : MonoBehaviour
{
    public List<Transform> enemySpawns;

    public List<GameObject> enemyTypes;

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

    IEnumerator enemySpawnCoroutine()
    {

        for (int i = 0; i < enemySpawnNumber; i++)
        {
            yield return new WaitForSeconds(spawnRate + index.idx.randomSign() * Random.Range(0, spawnRateAmplitude));

            enemies.Add(Instantiate(index.idx.randomChoice(enemyTypes), index.idx.randomChoice(enemySpawns).position, Quaternion.identity));
        }

        StartCoroutine(waitforenemiestodie());
    }

    IEnumerator waitforenemiestodie() 
    {
        while (enemies.Count > 0) { yield return new WaitForEndOfFrame(); }
        //opendoors();
    }

    public void initializeRoom()
    {
        if (coroutineEnded)
        {
            return;
        }

        // close doors
        // add door close function

        StartCoroutine(enemySpawnCoroutine());

        coroutineEnded = true;
    }
}
