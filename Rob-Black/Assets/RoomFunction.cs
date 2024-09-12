using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFunction : MonoBehaviour
{
    public bool nonattackingroom;

    public GameObject stairs;

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

        if (nonattackingroom)
        {
            roomFinished = true;
        }

        liveEnemies = roomInit.enemySpawnNumber;
        
    }

    private void Update()
    {
        if (stairs != null)
        {
            if (!index.idx.bossuiscript.bossbarvisible)
            {
                roomFinished = true;
            }
        }

        if (roomFinished) {
            lockWholeRoom = false;                
            stairs.SetActive(true);
            try
            {

            }
            catch { }

            return; }

        else
        {
            try
            {
                stairs.SetActive(false);
            }
            catch { }
        }

        try
        {
            liveEnemies = transform.parent.GetComponentsInChildren<DamageManager>().Length;
            roomText.text = liveEnemies.ToString();

            if (roomStarted && liveEnemies == 0)
            {
                lockWholeRoom = false;
                roomFinished = true;
            }
        }

        catch { }
    }
}
