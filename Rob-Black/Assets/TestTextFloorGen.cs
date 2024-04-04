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

                Vector3 bossroom = new Vector3(0,0,0);
                int bossroomnum = 0;
                int bossroomindex = 0;

                List<GameObject> rooms = new() { };

                foreach (List<FloorGenerator.roomstruct> roomlist in floor.rooms)
                {


                    foreach (FloorGenerator.roomstruct room in roomlist)
                    {

                        x -= 33;

                        if (room.prefab != null) {
                            GameObject rm = Instantiate(selectroom(room.roomnum));
                            rm.transform.position = new Vector3(x, 0, y);

                            if (room.roomnum > bossroomnum)
                            {
                                bossroomnum = room.roomnum;
                                bossroomindex = rooms.Count;
                                bossroom = rm.transform.position;
                            }

                            rooms.Add(rm);


                        }


                    }

                    x = 0;

                    y -= 17f;

                }

                Destroy(rooms[bossroomindex]);
                GameObject nrm = Instantiate(index.idx.randomroom(Boss));
                nrm.transform.position = bossroom;

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
