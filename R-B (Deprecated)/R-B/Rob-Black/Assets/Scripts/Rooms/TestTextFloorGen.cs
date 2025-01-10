using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTextFloorGen : MonoBehaviour
{
    FloorGenerator fgen;

    public List<GameObject> Normal;
    public List<GameObject> Boss;
    public List<GameObject> Store;
    public List<GameObject> Item;
    public List<GameObject> StartRooms;

    // public index index;

    GameObject selectroom(int roomtype)
    {
        switch (roomtype)
        {
            case -2:
                return index.idx.randomroom(Store);

            case -3:
                return index.idx.randomroom(Item);

            default:
                return index.idx.randomroom(Normal);
           

        }
       
    }

    // Start is called before the first frame update
    void Start() // init script to generate floor at game start
    {
        index.idx.floormanager.resetrooms();
        index.idx.floormanager.newfloor(1, index.idx.Player.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
