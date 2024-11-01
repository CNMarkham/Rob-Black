using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicPickupObject : MonoBehaviour
{
    public abstract void OnPickup();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPickup();
        }
    }
}
