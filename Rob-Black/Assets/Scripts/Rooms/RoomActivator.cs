using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // If touching room initialize room
    {
        if (other.CompareTag("Room"))
        {
            PlayerAttributes pa = index.idx.Player.GetComponent<PlayerAttributes>();
            if (pa!=null) pa.currentRoom = other.gameObject;
            
            KeepEnemiesInRoom.keepEntityInRoom(index.idx.Player.gameObject, other.gameObject.GetComponent<BoxCollider>(), other.gameObject);

            RoomInit ri = other.GetComponent<RoomInit>();
            if (ri!=null) ri.initializeRoom();

        }
    }
}
