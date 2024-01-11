using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class index : MonoBehaviour
{
    public GameObject SimpleBulletPrefab;

    public static index idx;

    public  Vector3 Round(Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    // Start is called before the first frame update
    void Start()
    {
        idx = FindObjectOfType<index>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
