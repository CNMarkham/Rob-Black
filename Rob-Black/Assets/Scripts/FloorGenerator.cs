using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{

    // Types of rooms
    // Str8 away <_>
    // corner |^>

    List<List<GameObject>> generateFloor() // Game objects are prefabs
    {
        int boundX = 5;
        int boundY = 5;

        int deviations = 2; // times it choses the least efficient one instaid of the most efficient one

        int specialRooms = 2;

        // Instansiate XxY matrix of null objects

        // Place spawn and end point (make sure they are at least boundX away from eachother)

        // Starting at spawn increment in every possible direction.
        
        // Find the ones that are the closest

        // add true in place of the closest one
        // if there are more than one closest one choose it randomly

        // loop through all of the true ones
        // determine wether or not it's a corner or str8 away
        // Choose one of them from the list of both types and set it to the prefab

        // randomly choose from prefabs and add special room in random direction untill specialrooms is 0

        return null;


    }
}
