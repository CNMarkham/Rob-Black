using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerLookAt : MonoBehaviour
{
    // Update is called once per frame
    void Update() // rotates player to mouse position
    {
        // https://discussions.unity.com/t/rotate-on-one-axis-to-look-at-cursor/448803/3

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = transform.position.y;

        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 difference = (mouseWorldSpace - transform.position).normalized;

        float zangle = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(-90f, 0f, -zangle + 180);

        /*
        Quaternion oldrotation = transform.rotation;

        Vector3 upAxis = new Vector3(0, 0, -1);
        Vector3 mouseScreenPosition = Input.mousePosition;
        
        mouseScreenPosition.z = transform.position.y;
        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        //print(mouseWorldSpace);

        Vector3 directionmouse = (transform.position - mouseWorldSpace).normalized;

        float angletomouse = Vector3.Angle(-transform.right, directionmouse);

        float atm = Vector3.Angle(transform.forward, directionmouse);

        // transform.LookAt(mouseWorldSpace, upAxis);
        transform.rotation = Quaternion.Euler(-90, 0, atm);

        //transform.eulerAngles = new Vector3(-90, 0, angletomouse);// -transform.eulerAngles.z + 90);

        //transform.rotation = Quaternion.Slerp(oldrotation, transform.rotation, Time.deltaTime);
        */
    }
}
