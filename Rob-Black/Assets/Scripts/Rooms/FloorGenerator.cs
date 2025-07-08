using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FloorGenerator : MonoBehaviour
{
    public enum Directions { Up, Down , Left, Right} // directions
    public enum roomtype { normal, boss, store, item, start , nule} // nule, in french, translates to "null" in english; an empty room

    public struct roomstruct
    {
        public Dictionary<Directions, bool> validdirections;
        public GameObject prefab;
        public roomtype roomtype;
        public int roomnum;

        public override string ToString()
        {
            if (roomnum < -1)
            {
                if (roomnum==-2)
                {
                    return "S";
                }

                if (roomnum==-3)
                {
                    return "I";
                }
            }


            return roomnum.ToString();
        }
    } // room struct

    public struct position
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }
    } // position (x,y)

    public struct posint
    {
        public position position;
        public int integer;

        public override string ToString()
        {
            return position.ToString() + ":" + integer.ToString();
        }
    } // position with integer attached

    public struct floor
    {

        // list of lists of dictionaried of directions (of which doors to open) along with their room prefab
        public List<List<roomstruct>> rooms;

        // dictionary of positions with their room number so room 1 is 0,0
        public Dictionary<position, int> roomorder;

    } // floor struct

    position distbetweenpoints(position p1, position p2)
    {
        return new position { x = Mathf.Abs(p1.x - p2.x), y = Mathf.Abs(p1.y - p2.y) };
    } // distance between points as point (x dist, y dist)

    int distbetweenpointsint(position p1, position p2) // distance between points
    {
        position p = distbetweenpoints(p1, p2);

        return Mathf.Abs(p.x) + Mathf.Abs(p.y);
    }

    bool outofbounds(int boundx, int boundy, position point)
    {
        if (point.x >= boundx || point.x < 0)
        {
            return true;
        }

        if (point.y >= boundy || point.y < 0)
        {
            return true;
        }

        return false;
    } // checks if point (x,y) is within bounds of rectangle (a,b)

    position p(int x, int y) { return new position { x = x, y = y }; } // shorthand for new position

    roomstruct r(GameObject prefab, roomtype roomtype, int roomnum) // Shorthand for new roomstruct
    { return new roomstruct { prefab = prefab, roomtype = roomtype, roomnum = roomnum };  }


    roomstruct nullrm() { return new roomstruct { prefab = null, roomtype= roomtype.nule }; }

    public floor generateFloor(index idx, List<GameObject> storerooms, List<GameObject> itemrooms) // Game objects are prefabs

    {
        int boundX = 8; // max: 100, min: 8
        int boundY = 10; // max: 100, min: 10

        int deviations = 10; // times it choses the least efficient one instaid of the most efficient one

        int specialRooms = 4;

        int storesAllowed = 1;

        Dictionary<position, int> roomorder = new();
        List<List<roomstruct>> roomsonlevel = new();

        position spawnpos = p(0, 0);
        position endpos;

        // initialize spawnposition and end position

        Dictionary<bool, int> bindict = new();

        bindict.Add(true, 1);
        bindict.Add(false, 0);

        // true: 1, false: 0

        Dictionary<bool, List<GameObject>> spcdict = new();

        spcdict.Add(true, storerooms);
        spcdict.Add(false, itemrooms);

        // true: store rooms[gameobject], false: item rooms[gameobject]

        Dictionary<bool, roomtype> spctydict = new();

        spctydict.Add(true, roomtype.store);
        spctydict.Add(false, roomtype.item);

        // spcdict but returns roomtype enum not array

        Dictionary<roomtype, int> spctyindict = new();

        spctyindict.Add(roomtype.store, -2);
        spctyindict.Add(roomtype.item, -3);

        // Instansiate XxY matrix of null objects

        for (int x = 0; x < boundX; x++)
        {

            List<roomstruct> row = new();

            for (int y = 0; y < boundY; y++)
            {  
                row.Add(nullrm());
            }

            roomsonlevel.Add(row);

        }

        // np.zeroes((x,y)) but with rooms instead of 0

        // init start level

        roomsonlevel[0][0] = r(index.idx.Player.gameObject, roomtype.start, 1);

        // set z to biggest num between bX & bY

        int z = Mathf.Max(boundX, boundY);

        // Place spawn and end point (make sure they are at least z away from eachother)

        int tempendposX = 0;
        int tempendposY = 0;

        for (int w = 0; w < z - 1; w++)
        {
            if (mathindex.randomBool())
            {

                tempendposX += 1;

                if (tempendposX > boundX - 1)
                {
                    tempendposX -= 1;
                    tempendposY += 1;

                }


            }

            else
            {

                tempendposY += 1;

                if (tempendposY > boundY - 1)
                {
                    tempendposY -= 1;
                    tempendposX += 1;

                }
            }

        }

        endpos = p(tempendposX, tempendposY);

        // Starting at spawn increment in every possible direction.

        List<position> possiblities = new();
        position currentroom = spawnpos;
        int currentroomnum = 1;

        roomorder[currentroom] = currentroomnum;

        try
        {
            while (true)
            {
                currentroomnum += 1;
                possiblities = new();

                possiblities.Add(p(currentroom.x, currentroom.y + 1));
                possiblities.Add(p(currentroom.x, currentroom.y - 1));

                possiblities.Add(p(currentroom.x + 1, currentroom.y));
                possiblities.Add(p(currentroom.x - 1, currentroom.y));

                List<position> modifiedpossibilities = new List<position>(possiblities);

                foreach (position posibility in possiblities)
                {
                    if (outofbounds(boundX, boundY, posibility))
                    {
                        modifiedpossibilities.Remove(posibility);
                    }

                    if (roomorder.ContainsKey(posibility))
                    {
                        modifiedpossibilities.Remove(posibility);
                    }
                }

                List<posint> possibilitywithdist = new();

                foreach (position posibility in modifiedpossibilities)
                {
                    possibilitywithdist.Add(new posint { position = posibility, integer = distbetweenpointsint(posibility, endpos) });
                }

                List<posint> orderedpossibilities = possibilitywithdist.OrderBy(o => o.integer).ToList();
                if (orderedpossibilities.Count <= 0) { break; }
                position chosenposibility = orderedpossibilities[0].position;

                if (orderedpossibilities.Contains(new posint { position = endpos, integer = 0 }))
                {
                    roomsonlevel[endpos.x][endpos.y] = r(index.idx.Player.gameObject, roomtype.start, currentroomnum);
                    break;
                }

                if (deviations > 0)
                {
                    if (mathindex.randomBool() && mathindex.randomBool())
                    {
                        deviations -= 1;

                        chosenposibility = orderedpossibilities[orderedpossibilities.Count - 1].position;

                    }
                }

                roomsonlevel[chosenposibility.x][chosenposibility.y] = r(index.idx.Player.gameObject, roomtype.start, currentroomnum);

                roomorder[chosenposibility] = currentroomnum;

                currentroom = chosenposibility;
            }

            // the purpose of this dict is to cache the previously used rooms and restart the whole thing if it didn't work
            Dictionary<position, bool> ouestcache = new();

            for (int i = 0; i < specialRooms; i++)
            {
                var added = false;

                while (!added)
                {
                    var ouest = roomorder.ToList()[Random.Range(0, roomorder.Count - 1)].Key;

                    if (ouestcache.ContainsKey(ouest)) { continue; }

                    var x = ouest.x;
                    var y = ouest.y;

                    var roomtbp = roomsonlevel[x][y];

                    if (y == 0 && x == 0) { continue; }

                    List<position> npossibilities = new List<position>() { };

                    npossibilities.Add(p(x, y + 1));

                    npossibilities.Add(p(x, y - 1));

                    npossibilities.Add(p(x + 1, y));
                    npossibilities.Add(p(x - 1, y));

                    int newx = -1;
                    int newy = -1;


                    for (int j = 0; j < npossibilities.Count; j++) // loop through possible rooms
                    {

                        if (!roomorder.ContainsKey(npossibilities[j])) // Check if room is in bounds
                        {
                            newx = npossibilities[j].x;
                            newy = npossibilities[j].y;
                            break;
                        }
                    }

                    var rmtype = mathindex.randomBool();

                    if (storesAllowed <= 0)
                    {
                        rmtype = false;
                    }

                    else
                    {
                        if (rmtype == true)
                        {
                            storesAllowed -= 1;
                        }
                    }

                    ouestcache.Add(ouest, true);
                    if (outofbounds(boundX, boundY, ouest))
                    {
                        continue;
                    }

                    roomorder[p(newx, newy)] = spctyindict[spctydict[rmtype]];

                    var prefab = mathindex.randomroom(spcdict[rmtype]);
                    if (newy <= roomsonlevel.Count || newx <= roomsonlevel[0].Count) { break; }
                    ;
                    print(newx);
                    print(newy);
                    roomsonlevel[newx][newy] = r(prefab, spctydict[rmtype], spctyindict[spctydict[rmtype]]);
                    // raandomly choses room of roomtype room if room is room then store elsre not store and therefore item

                    added = true;

                }

            }

            // randomly choose from prefabs and add special room in random direction untill specialrooms is 0

            return new() { rooms = roomsonlevel };

        }
        catch { return generateFloor(idx, storerooms, itemrooms); }
    }

}
