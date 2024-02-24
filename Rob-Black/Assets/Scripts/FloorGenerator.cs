using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FloorGenerator : MonoBehaviour
{

    // TODO: add seeds -- replace random func with Psudo random func


    // Types of rooms
    // Str8 away <_>
    // corner |^>
    public enum Directions { Up, Down , Left, Right}
    public enum roomtype { normal, boss, store, item, start }

    public struct roomstruct
    {
        public Dictionary<Directions, bool> validdirections;
        public GameObject prefab;
        public roomtype roomtype;
        public int roomnum;

        public override string ToString()
        {
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

    roomstruct nullrm() { return new roomstruct { prefab = null }; }

    public floor generateFloor() // Game objects are prefabs

    {
        int boundX = 5;
        int boundY = 5;

        int deviations = 2; // times it choses the least efficient one instaid of the most efficient one

        int specialRooms = 2;

        int storesAllowed = 1;

        Dictionary<position, int> roomorder = new();
        List<List<roomstruct>> roomsonlevel = new();

        position spawnpos = p(0, 0);
        position endpos;

        Dictionary<bool, int> bindict = new();

        bindict.Add(true, 1);
        bindict.Add(false, 0);

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
            if (index.idx.randomBool())
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
        roomsonlevel[endpos.x][endpos.y] = r(d(true, false, false, true), new GameObject(), roomtype.start, z);

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
                break;
            }

            if (deviations > 0)
            {
                if (index.idx.randomBool() && index.idx.randomBool())
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

        // randomly choose from prefabs and add special room in random direction untill specialrooms is 0

        return new();


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
