using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class door : MonoBehaviour
{
    //public bool locked;
    public BoxCollider lockCollider;
    public RoomFunction room;
    
    public SpriteRenderer doorColourController;
    public Color doorClosedColour;
    public Color doorOpenColour;

    public GameObject doorpeer;

    private void Start()
    {
        //lockCollider.enabled = locked;

        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 100.0F);
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != transform)
            {
                if (hit.collider.gameObject.CompareTag("Door"))
                {
                    doorpeer = hit.collider.gameObject;
                }
            }

        }
    }

    private void Update()
    {

        if (room)
        {
            lockCollider.enabled = room.lockWholeRoom;

        }


        door doorpeerdoorscript = null;

        try {
           doorpeerdoorscript  = doorpeer.GetComponent<door>();
        }
        catch { }

        print("not broken");

        // CHECK IF THERE IS A DOOR PEER

        // IF THERE IS A DOOR PEER
        // DOOR PEER IS LOCKED: TRANSPARENT
        // DOOR PEER IS UNLOCKED: CLOSED

        // IF THERE ISN'T A DOOR PEER
        // ALWAYS LOCKED

        if (!doorpeerdoorscript) {

            // lockCollider.enabled = false;
            doorColourController.color = doorClosedColour;

        }

        else
        {
            if (doorpeerdoorscript.lockCollider.enabled)
            {
                doorColourController.color = new Color(0, 0, 0, 0);
            }

            else
            {
                doorColourController.color = doorOpenColour;
            }
        }

        if (room)
        {
            if (!room.lockWholeRoom && room.roomFinished) { doorColourController.color = doorClosedColour; }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            doorpeer = other.gameObject;
        }
    }
}
