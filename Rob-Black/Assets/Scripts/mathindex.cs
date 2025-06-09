using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mathindex : MonoBehaviour
{
    public static T randomChoice<T>(List<T> list) // import random; return random.choice
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static int randomInteger(int x, int y) // import random; return random.randint
    {
        return UnityEngine.Random.Range(x, y);
    }

    public static GameObject randomroom(List<GameObject> rooms) // Random Choice with added syntax sugar
    {
        return rooms[UnityEngine.Random.Range(0, rooms.Count)];
    }

    public static Vector3 Round(Vector3 vector3, int decimalPlaces = 2) // round V3 to x decimal points
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    public static bool randdict(int key)
    {
        if (key == 1) return true;
        return false;
    }


    public static bool randomBool() // import random; {1: True, 0: False}[random.randint(0,1)]
    {
        return randdict(UnityEngine.Random.Range(0, 2));
    }

    public static float randomSign() // import random; {True: 1, False: -1}[{1: True, 0: False}[random.randint(0,1)]]
    {

        if (randomBool()) return 1f;
        return -1f;
    }

}