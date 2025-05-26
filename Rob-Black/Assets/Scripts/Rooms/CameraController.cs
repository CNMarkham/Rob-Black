using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Cinemachine.CinemachineConfiner confiner;

    private void OnTriggerEnter(Collider other) // sets the bounding room when the player touches the inside
    {
        if (other.CompareTag("Room"))
        {
            confiner.m_BoundingVolume = other.GetComponent<Collider>();
        }
    }
}
