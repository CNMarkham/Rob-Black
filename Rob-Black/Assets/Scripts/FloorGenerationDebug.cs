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

    public void genera()
    {
        index.idx.FloorGeneratorIndex.generateFloor(index, index.storeRooms, index.itemRooms);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
