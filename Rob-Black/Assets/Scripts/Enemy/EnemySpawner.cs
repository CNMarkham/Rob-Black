using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // debug func/obj to spawn enemies repeatedly

    public int spawnInterval;
    public GameObject enemyPrefab;

    private int timeinms;

    private void Update()
    { 
        timeinms = (int)Mathf.Floor(Time.time * 1000);

        if (timeinms % spawnInterval == 0)
        {

            Instantiate(enemyPrefab, gameObject.transform);
            
        }
    }

}
