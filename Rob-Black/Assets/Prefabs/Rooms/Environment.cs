using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Environment", order = 1)]

public class Environment : ScriptableObject
{
    public index.environment environment;

    [Header("Rooms")]
    public List<GameObject> normalRooms;
    public List<GameObject> bossRooms;
    public List<GameObject> storeRooms;
    public List<GameObject> itemRooms;
    public List<GameObject> startRooms;

}
