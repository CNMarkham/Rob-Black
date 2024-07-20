using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFunction : MonoBehaviour
{
    public RoomInit roomInit;
    public bool roomStarted;
    public bool roomFinished;

    public int liveEnemies;
    public TMPro.TMP_Text roomText;

    public List<GameObject> doors;

    public bool lockWholeRoom;
    private bool prevLockWholeRoom;

    private void Start()
    {
        roomStarted = false;
        roomFinished = false;

        liveEnemies = roomInit.enemySpawnNumber;
        
    }

    private void Update()
    {
        liveEnemies = transform.parent.GetComponentsInChildren<DamageManager>().Length;
        roomText.text = liveEnemies.ToString();

        if (roomStarted && liveEnemies == 0)
        {
            lockWholeRoom = false;
            roomFinished = true;
        }
    }
}
