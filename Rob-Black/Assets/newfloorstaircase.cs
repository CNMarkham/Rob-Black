using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newfloorstaircase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        index.idx.floormanager.resetrooms();
        index.idx.floormanager.newfloor(1, collision.gameObject.transform.position);
    }
}
