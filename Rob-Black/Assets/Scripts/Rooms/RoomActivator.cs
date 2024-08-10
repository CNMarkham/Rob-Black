using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // If touching room initialize room
    {
        if (other.CompareTag("Room"))
        {
            try
            {
                index.idx.Player.GetComponent<PlayerAttributes>().currentRoom = other.gameObject;
                KeepEnemiesInRoom.keepEntityInRoom(index.idx.Player.gameObject, other.gameObject.GetComponent<BoxCollider>(), other.gameObject);

                other.GetComponent<RoomInit>().initializeRoom();
            }

            catch
            {

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
