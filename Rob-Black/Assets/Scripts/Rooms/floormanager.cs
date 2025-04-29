using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floormanager : MonoBehaviour
{
    FloorGenerator fgen;

    public List<Environment> environments;

    public Environment setenvironment;

    // public index index;

    GameObject selectroom(int roomtype) // roomtypeint -> gameobject instance from rooms list
    {

        switch (roomtype)
        {
            case 1:
                return index.idx.randomroom(setenvironment.startRooms);

            case -2:
                return index.idx.randomroom(setenvironment.storeRooms);

            case -3:
                return index.idx.randomroom(setenvironment.itemRooms);

            default:
                return index.idx.randomroom(setenvironment.normalRooms);


        }

    }

    public IEnumerator stopblack(int delayms) // corutine to stop the screen from being black
    {
        yield return new WaitForSeconds(delayms / 1000);
        index.idx.screenblack(false);
    }

    public void resetrooms() // kill all money, sets screen black, finds all objects with room tags, deletes them then starts the
                             // 1000 ms corutine to stop the screen from being black
    {

        index.idx.kill_bill(); // It is strange to see money you stole on the *next* floor

        index.idx.screenblack(true);

        //System.Threading.Thread.Sleep(100);

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        foreach (GameObject room in rooms)
        {
            Destroy(room);
        }

        StartCoroutine(stopblack(1000));

    }

    public void newfloor(int level, Vector3 position) // Using the position vector as a root it uses the floor generation script attatched to the 
                                                      // index file to generate a room and then it enumerates all of the rooms in newley generated floor
                                                      // in order to place them aroiund the position

    {
        Environment currentenvironment = environments[PlayerFloorCount.floorNumber + 3 % index.idx.availableenvironments.Count];

        setenvironment = currentenvironment;
        index.idx.currentenvironment = currentenvironment.environment;


        PlayerFloorCount.floorNumber += 1;

        while (true)
        {

            try
            {

                while (FindObjectOfType<index>() == null) { };

                index index = FindObjectOfType<index>();

                fgen = gameObject.GetComponent<FloorGenerator>();

                FloorGenerator.floor floor = fgen.generateFloor(index, index.storeRooms, index.itemRooms);

                float x = position.x + 33;
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

                    x = 33;

                    y -= 17f;

                }

                Destroy(rooms[bossroomindex]);
                GameObject nrm = Instantiate(index.idx.randomroom(setenvironment.bossRooms));
                nrm.transform.position = bossroom;

                index.idx.screenblack(false);

                return;
            }

            catch
            { //print(e.ToString()); 
                Application.Quit();
            }
        }

    }
}
