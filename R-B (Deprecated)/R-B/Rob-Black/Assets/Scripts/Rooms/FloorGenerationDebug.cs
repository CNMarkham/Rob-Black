using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerationDebug : MonoBehaviour
{
    public index index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void genera() // debug to generate floor waaay back in the day
    {
        index.idx.FloorGeneratorIndex.generateFloor(index, index.storeRooms, index.itemRooms);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
