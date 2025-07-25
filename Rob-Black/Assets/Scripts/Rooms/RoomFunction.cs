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

        if (!nonattackingroom) {
            liveEnemies = roomInit.enemySpawnNumber;
        }
    }

    private void Update()
    {
        if (stairs != null)
        {
            try
            {
                if (index.idx.bossuiscript.bossObject == null && roomStarted)
                {
                    roomFinished = true;
                }
            }

            catch { }


        }

        if (roomFinished) {
            lockWholeRoom = false;                

            if (stairs!=null) stairs.SetActive(true);

            return; }

        else
        {                
            
            if (stairs != null) stairs.SetActive(false);

        }

        liveEnemies = transform.parent.GetComponentsInChildren<DamageManager>().Length;
        roomText.text = liveEnemies.ToString();

        if (roomStarted && liveEnemies == 0)
        {
            lockWholeRoom = false;
            roomFinished = true;
        }
    }
}
