using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floormanager : MonoBehaviour
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

    public IEnumerator stopblack(int delayms)
    {
        yield return new WaitForSeconds(delayms / 1000);
        index.idx.screenblack(false);
    }

    public void resetrooms()
    {

        index.idx.kill_bill(); // It is strange to see money you stole one the *next* floor

        index.idx.screenblack(true);

        System.Threading.Thread.Sleep(100);

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        foreach (GameObject room in rooms)
        {
            Destroy(room);
        }

        StartCoroutine(stopblack(1000));

    }

    public void newfloor(int level, Vector3 position)
    {

        PlayerFloorCount.floorNumber += 1;

        while (true)
        {

            try
            {
                // doesn't work because this runs before index is finished initializing

                while (FindObjectOfType<index>() == null) { };

                index index = FindObjectOfType<index>();

                fgen = gameObject.GetComponent<FloorGenerator>();

                FloorGenerator.floor floor = fgen.generateFloor(index, index.storeRooms, index.itemRooms);

                float x = position.x;
                float y = position.y;

                Vector3 bossroom = new Vector3(0, 0, 0);
                int bossroomnum = 0;
                int bossroomindex = 0;

                List<GameObject> rooms = new() { };

                foreach (List<FloorGenerator.roomstruct> roomlist in floor.rooms)
                {


                    foreach (FloorGenerator.roomstruct room in roomlist)
                    {

                        x -= 33;

                        if (room.prefab != null)
                        {
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

                index.idx.screenblack(false);

                return;
            }

            catch (System.Exception e) { print(e.ToString()); Application.Quit(); }
        }

    }
}
