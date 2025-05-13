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

    public IEnumerator check2()
    {
        yield return new WaitForSeconds(2);

        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down * 2, 110.0F);

        Debug.DrawRay(transform.position + Vector3.up, Vector3.down * 2, Color.red, 110.0f);

        Debug.Break();

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

    private void Start()
    {
        //lockCollider.enabled = locked;

        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down*2, 110.0F);

        Debug.DrawRay(transform.position + Vector3.up, Vector3.down*2, Color.red, 110.0f);

        //Debug.Break();
        
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

        StartCoroutine(check2());
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

        //print("not broken");

        // CHECK IF THERE IS A DOOR PEER

        // IF THERE IS A DOOR PEER
        // DOOR PEER IS LOCKED: TRANSPARENT
        // DOOR PEER IS UNLOCKED: CLOSED

        // IF THERE ISN'T A DOOR PEER
        // ALWAYS LOCKED

        /*
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
        */

        if (lockCollider.enabled) { doorColourController.color = doorClosedColour; } // if locked use locked color otherwise open color
        else { doorColourController.color = doorOpenColour; }

        if (doorpeerdoorscript!=null) // check if doorpeer exists
        {
            if (doorpeerdoorscript.lockCollider.enabled) { doorColourController.color = doorClosedColour; };
        }

        else // if not, close door
        {
            doorColourController.color = doorClosedColour;
            lockCollider.enabled = true;
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
