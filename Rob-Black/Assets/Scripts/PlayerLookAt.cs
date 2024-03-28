using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Quaternion oldrotation = transform.rotation;

        Vector3 upAxis = new Vector3(0, 0, -1);
        Vector3 mouseScreenPosition = Input.mousePosition;
        
        mouseScreenPosition.z = transform.position.y;
        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        //print(mouseWorldSpace);
        transform.LookAt(mouseWorldSpace, upAxis);

        transform.eulerAngles = new Vector3(-90, 0, -transform.eulerAngles.z + 90);

        transform.rotation = Quaternion.Slerp(oldrotation, transform.rotation, 0.1f);
    }
}
