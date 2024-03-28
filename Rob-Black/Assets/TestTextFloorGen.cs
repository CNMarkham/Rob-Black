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
                return Normal[0];
           

        }
       
    }

    // Start is called before the first frame update
    void Start()
    {

        while (true) {

            try
            {
                // doesn't work because this runs before index is finished initializing

                while (FindObjectOfType<index>() == null) { };

                index index = FindObjectOfType<index>();

                fgen = gameObject.GetComponent<FloorGenerator>();

                FloorGenerator.floor floor = fgen.generateFloor(index, index.storeRooms, index.itemRooms);

                float x = 0;
                float y = 0;

                foreach (List<FloorGenerator.roomstruct> roomlist in floor.rooms)
                {


                    foreach (FloorGenerator.roomstruct room in roomlist)
                    {

                        x -= 33;

                        if (room.prefab != null) {
                            GameObject rm = Instantiate(selectroom(room.roomnum));
                            rm.transform.position = new Vector3(x, 0, y);
                        }


                    }

                    x = 0;

                    y -= 17f;

                }

                return;
            }

            catch { }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
