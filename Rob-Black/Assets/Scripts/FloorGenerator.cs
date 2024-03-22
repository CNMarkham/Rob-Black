using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FloorGenerator : MonoBehaviour
{

    // TODO: add seeds -- replace random func with Psudo random func


    // Types of rooms
    // Str8 away <_>
    // corner |^>
    public enum Directions { Up, Down , Left, Right}
    public enum roomtype { normal, boss, store, item, start , nule}

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
    }

    public struct position
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }
    }

    public struct posint
    {
        public position position;
        public int integer;

        public override string ToString()
        {
            return position.ToString() + ":" + integer.ToString();
        }
    }

    public struct floor
    {

        // list of lists of dictionaried of directions (of which doors to open) along with their room prefab
        public List<List<roomstruct>> rooms;

        // dictionary of positions with their room number so room 1 is 0,0
        public Dictionary<position, int> roomorder;

    }

    position distbetweenpoints(position p1, position p2)
    {
        return new position { x = Mathf.Abs(p1.x - p2.x), y = Mathf.Abs(p1.y - p2.y) };
    }

    int distbetweenpointsint(position p1, position p2)
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
    }

    position p(int x, int y) { return new position { x = x, y = y }; }

    roomstruct r(Dictionary<Directions, bool> validdirections, GameObject prefab, roomtype roomtype, int roomnum) 
    { return new roomstruct { validdirections = validdirections, prefab = prefab, roomtype = roomtype, roomnum = roomnum };  }

    Dictionary<Directions, bool> d(bool up, bool down, bool left, bool right) 
    {

        Dictionary<Directions, bool> nd = new();

        nd.Add(Directions.Up, up);
        nd.Add(Directions.Down, down);
        nd.Add(Directions.Left, left);
        nd.Add(Directions.Right, right);

        return nd;

    }

    roomstruct nullrm() { return new roomstruct { prefab = null, roomtype= roomtype.nule }; }

    public floor generateFloor(index idx, List<GameObject> storerooms, List<GameObject> itemrooms) // Game objects are prefabs

    {
        int boundX = 8;
        int boundY = 10;

        int deviations = 10; // times it choses the least efficient one instaid of the most efficient one

        int specialRooms = 4;

        int storesAllowed = 1;

        Dictionary<position, int> roomorder = new();
        List<List<roomstruct>> roomsonlevel = new();

        position spawnpos = p(0, 0);
        position endpos;

        Dictionary<bool, int> bindict = new();

        bindict.Add(true, 1);
        bindict.Add(false, 0);

        Dictionary<bool, List<GameObject>> spcdict = new();

        spcdict.Add(true, storerooms);
        spcdict.Add(false, itemrooms);

        Dictionary<bool, roomtype> spctydict = new();

        spctydict.Add(true, roomtype.store);
        spctydict.Add(false, roomtype.item);

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

        print(roomsonlevel.Count);
        print(roomsonlevel[0].Count);

        // init start level

        roomsonlevel[0][0] = r(d(false, true, false, true), new GameObject(), roomtype.start, 1);

        // set z to biggest num between bX & bY

        int z = Mathf.Max(boundX, boundY);


        // Place spawn and end point (make sure they are at least z away from eachother)

        int tempendposX = 0;
        int tempendposY = 0;

        for (int w = 0; w < z - 1; w++)
        {
            if (idx.randomBool())
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

        //print(tempendposY);
        //print(tempendposX);

        endpos = p(tempendposX, tempendposY);

        // Starting at spawn increment in every possible direction.

        List<position> possiblities = new();
        position currentroom = spawnpos;
        int currentroomnum = 1;

        roomorder[currentroom] = currentroomnum;

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

                if (roomorder.ContainsKey(posibility)) {
                    modifiedpossibilities.Remove(posibility);
                }
            }

            print("modified possibilities: " + coolprintlist(modifiedpossibilities));

            List<posint> possibilitywithdist = new();

            foreach (position posibility in modifiedpossibilities)
            {
                possibilitywithdist.Add(new posint { position = posibility, integer = distbetweenpointsint(posibility, endpos)});
            }

            print("possibilitywithdist: " + coolprintlist(possibilitywithdist));

            List<posint> orderedpossibilities = possibilitywithdist.OrderBy(o => o.integer).ToList();
            position chosenposibility = orderedpossibilities[0].position;

            print("orderedpossibilities: " + coolprintlist(orderedpossibilities));
            print("chosenpossibility0: " + chosenposibility);


            if (orderedpossibilities.Contains(new posint { position=endpos, integer=0 }))
            {
                roomsonlevel[endpos.x][endpos.y] = r(d(true, false, false, true), new GameObject(), roomtype.start, currentroomnum);
                break;
            }

            if (deviations > 0)
            {
                if (idx.randomBool() && idx.randomBool())
                {
                    deviations -= 1;

                    chosenposibility = orderedpossibilities[orderedpossibilities.Count - 1].position;

                } 
            }

            //var roomsonlevelint = new List<List<roomstruct>>(roomsonlevel);
            //roomsonlevelint.ForEach(o => o.ForEach(o => o.tbp=bindict[!o.prefab.Equals(null)].ToString()));

            roomsonlevel[chosenposibility.x][chosenposibility.y] = r(d(true, false, false, true), new GameObject(), roomtype.start, currentroomnum);

            roomorder[chosenposibility] = currentroomnum;
            print("roomorder: " + coolprintdict(roomorder));
            print("roomsonlevel: " + coolprintmatrix(roomsonlevel));
            print("chosen possibility: " + chosenposibility);
            //roomsonlevel[chosenposibility.x][chosenposibility.y] = r(, , roomtype.normal);

            currentroom = chosenposibility;
        }

        // the p[eorodksk of thios porem os to cahs the previously used rooms and restsart the whole thing if it didn't work
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
                print(new Vector2Int(x, y));
                npossibilities.Add(p(x, y - 1));

                npossibilities.Add(p(x + 1, y));
                npossibilities.Add(p(x - 1, y));
                print("possibilities: " + coolprintlist(npossibilities));

                int newx = -1;
                int newy = -1;

                // var possibility = npossibilities[Random.Range(0, npossibilities.Count - 1)];

                // test wjhile lopoopeppefs


                for (int j = 0; j < npossibilities.Count; j++) // loop through possible rooms
                {
                    print("IN For Loop");
                    if (!roomorder.ContainsKey(npossibilities[j])) // Check if room is in bounds
                    {
                        print("dididnt Contained Key");
                        // && roomsonlevel[npossibilities[j].x][npossibilities[j].y].roomtype == roomtype.nule
                        newx = npossibilities[j].x;
                        newy = npossibilities[j].y;
                        print("worked"); // Doesn't print TODO: fix
                        print("worked"); // Doesn't print TODO: fix
                        break;
                    }
                }

                print(newx);
                print(newy);





                var rmtype = idx.randomBool();

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


                roomsonlevel[newx][newy] = r(d(false, false, false, false), idx.randomroom(spcdict[rmtype]), spctydict[rmtype], spctyindict[spctydict[rmtype]]);
                // raandomly choses room of roomtype room if room is room then store elsre not store and therefore item

                added = true;

            }

            print("roomorder: " + coolprintdict(roomorder));
            print("roomsonlevel: " + coolprintmatrix(roomsonlevel));

         

        }



        // randomly choose from prefabs and add special room in random direction untill specialrooms is 0

        return new() { rooms = roomsonlevel };


    }

    public string generateStringFromFloor(floor floor)
    {
        string floorstring = "[\n";

        List<List<int>> tointlist = new();

        int rw = 0; // x
        int rm = 0; // y

        List<List<roomstruct>> roommatrix = floor.rooms;
        Dictionary<position, int> roomorder = floor.roomorder;

        foreach (List<roomstruct> row in roommatrix)
        {
            
            foreach(roomstruct room in row)
            {

                if (room.prefab != null)
                {
                    tointlist[rw][rm] = roomorder[new position { x = rw, y = rm }];
                }

                else
                {
                    tointlist[rw][rm] = 0;
                }

                rm++;
            }

            rw++;
        }

        foreach (List<int> row in tointlist)
        {
            string pls = "[ ";

            foreach (int x in row)
            {
                pls += x.ToString() + ", ";
            }

            pls += " ]";

            floorstring += pls + "\n";
        }

        floorstring += "\n]";

        return floorstring;


    }

    string coolprintdict<K, V>(Dictionary<K, V> dict)
    {

        var pls = "{\n";

        foreach (K key in dict.Keys)
        {
            pls += typeof(K).ToString() + " " + key.ToString() + ": " + typeof(V).ToString() + " " + dict[key].ToString() + "\n";
        }

        pls += "\n}";

        return pls;
    
    }

    string coolprintlist<T>(List<T> list)
    {
        var pls = "[ ";

        foreach (T item in list)
        {
            pls += item.ToString() + ", ";
        }

        pls += " ]";

        return pls;
    }

    string coolprintmatrix<T>(List<List<T>> list)
    {
        var pls = "[\n";

        foreach (List<T> item in list)
        {
            pls += coolprintlist(item) + "\n";
        }

        pls += "\n]";

        return pls;
    }

}
