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
