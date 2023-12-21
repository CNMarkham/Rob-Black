using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    // put this classs in an "index" class
    // that each class can access whenever it needs
    // so something like
    // index.simplebullet

    public float damage;
    public float knockback;

    // any more required attrs
    // public AudioSource shoot noise ...


    public float bulletspeed;
    public Transform rotation;

    private void Start()
    {
                                 
    }

    void Update()
    {
        transform.localPosition += rotation.right * bulletspeed * Time.deltaTime;

        // this.transform.rotation = rotation.rotation;
    }
}
