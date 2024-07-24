using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class door : MonoBehaviour
{
    public bool locked;
    public BoxCollider lockCollider;
    public RoomFunction room;
    
    public SpriteRenderer doorColourController;
    public Color doorClosedColour;
    public Color doorOpenColour;

    private void Start()
    {
        lockCollider.enabled = locked;
    }

    private void Update()
    {
        lockCollider.enabled = room.lockWholeRoom;
        
        if (locked) { doorColourController.color = doorClosedColour; }
        else { doorColourController.color = doorOpenColour; }
    }
}
