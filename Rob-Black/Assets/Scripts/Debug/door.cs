using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool locked;
    public BoxCollider lockCollider;
    public RoomFunction room;

    private void Start()
    {
        lockCollider.enabled = locked;
    }

    private void Update()
    {
        lockCollider.enabled = room.lockWholeRoom;
    }
}
