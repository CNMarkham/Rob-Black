using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    { 
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -(transform.position.x - Camera.main.transform.position.x);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        transform.LookAt(worldPos);
    }
}
