using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]

public class EnemySpawnModule : ScriptableObject
{
    public List<Transform> EnemySpawns;
    public List<GameObject> EnemyTypes;

    public index.difficulty difficulty;
    public index.environment environment;
    

}
