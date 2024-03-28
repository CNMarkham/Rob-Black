using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Cinemachine.CinemachineConfiner confiner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            confiner.m_BoundingVolume = other.GetComponent<Collider>();
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
