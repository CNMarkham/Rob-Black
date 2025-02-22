using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCount : MonoBehaviour
{
    public static int floorNumber;

    public TMPro.TMP_Text floornumtext;

    // Update is called once per frame
    void FixedUpdate()
    {
        floornumtext.text = floorNumber.ToString();
        // floorNumber = 3;
        // Debugging purposes
    }
}
