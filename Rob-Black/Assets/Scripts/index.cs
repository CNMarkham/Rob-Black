using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class index : MonoBehaviour
{
    public GameObject SimpleBulletPrefab;


    public static index idx;

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
