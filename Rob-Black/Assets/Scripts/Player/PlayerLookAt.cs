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

    }
}
