using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTextFloorGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() // init script to generate floor at game start
    {
        index.idx.floormanager.resetrooms();
        index.idx.floormanager.newfloor(1, index.idx.Player.transform.position);

    }
}
