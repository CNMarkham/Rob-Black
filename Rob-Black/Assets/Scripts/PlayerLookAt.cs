using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 upAxis = new Vector3(0, 0, -1);
        Vector3 mouseScreenPosition = Input.mousePosition;
        
        mouseScreenPosition.z = transform.position.z;
        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition).normalized * 7.5f;
        transform.LookAt(mouseWorldSpace, upAxis);

        transform.eulerAngles = new Vector3(-90, 0, -transform.eulerAngles.z + 90);
    }
}
