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

    GameObject selectroom(int roomtype)
    {
        switch (roomtype)
        {
            case -2:
                return index.idx.randomroom(Store);

            case -3:
                return index.idx.randomroom(Item);

            default:
                return Normal[0];
           

        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        // doesn't work because this runs before index is finished initializing

        fgen = gameObject.GetComponent<FloorGenerator>();

        FloorGenerator.floor floor = fgen.generateFloor();

        float x = 0;
        float y = 0;

        foreach (List<FloorGenerator.roomstruct> roomlist in floor.rooms)
        {


            foreach (FloorGenerator.roomstruct room in roomlist)
            {

                y += 5;

                GameObject rm = Instantiate(selectroom(room.roomnum));
                rm.transform.position = new Vector3(x, 0, y);
            }

            x += 5;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
