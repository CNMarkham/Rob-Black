using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{

    // Types of rooms
    // Str8 away <_>
    // corner |^>

    (int, int) distbetweenpoints(int x1, int y1, int x2, int y2)
    {
        return (Mathf.Abs(x1 - x2), Mathf.Abs(y1 - y2));
    }

    bool outofbounds(int boundx, int boundy, (int, int) point)
    {
        if (point.Item1 >= boundx || point.Item1 < 0)
        {
            return true;
        }

        if (point.Item2 >= boundy || point.Item2 < 0)
        {
            return true;
        }

        return false;
    }

    (int, int) addintint((int, int) in1, (int, int) in2)
    {
        return (in1.Item1 + in2.Item1, in1.Item1 + in2.Item2);
    }
    
    (int,int) observe((int, int) x) { return x; }

    public (List<List<GameObject>>, Dictionary<(int,int) ,int>) generateFloor() // Game objects are prefabs
        // list<go> is the list of prefabs and positions
        // dict<pos,i> is the position then the number of the room so like room 1 is 0,0

    {
        int boundX = 5;
        int boundY = 5;

        int deviations = 2; // times it choses the least efficient one instaid of the most efficient one

        int specialRooms = 2;

        Dictionary<bool, (int, int)> appdict = new();

        appdict.Add(true, (1, 0));
        appdict.Add(false, (0, 1));

        Dictionary<(int, int), int> rooms = new();

        (int, int) spawnpos = (0, 0);
        (int, int) endpos = (0, 0);

        // Instansiate XxY matrix of null objects

        List<List<GameObject>> matrix = new List<List<GameObject>>();

        for (int x = 0; x < boundX; x++)
        {
            var newar = new List<GameObject>();
            
            for (int y = 0; y < boundY; y++)
            {
                newar.Add(null);
            }

            matrix.Add(newar);
        }

        print(matrix.Count);
        print(matrix[0].Count);
        // Place spawn and end point (make sure they are at least boundX away from eachother)

        for (int z = 0; z < boundX; z++)
        {
            var newbool = index.idx.randomBool();

            var newpos = endpos;

            switch (newbool)
            {
                case true:
                    newpos.Item1 += 1;
                    break;
                case false:
                    newpos.Item2 += 1;
                    break;
            }

            endpos = newpos;

        }

        print(endpos);

        // Starting at spawn increment in every possible direction.

        //while (\) {
            // Find the ones that are the closest

            //List

            // add true in place of the closest one
            // if there are more than one closest one choose it randomly

            // loop through all of the true ones
            // determine wether or not it's a corner or str8 away
            // Choose one of them from the list of both types and set it to the prefab
        //}

        // randomly choose from prefabs and add special room in random direction untill specialrooms is 0

        return (new(), new());


    }

    public string generateStringFromFloor(List<List<GameObject>> floor, Dictionary<(int,int), int> roomnum)
    {
        string floorstring = "";

        List<List<int>> tointlist = new();

        int rw = 0; // x
        int rm = 0; // y

        foreach (List<GameObject> row in floor)
        {
            
            foreach(GameObject room in row)
            {

                if (room != null)
                {
                    tointlist[rw][rm] = roomnum[(rw, rm)];
                }

                else
                {

                }

                rm++;
            }

            rw++;
        }



        return null;


    }
}
