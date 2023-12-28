using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float damage;
    public float knockback;

    // any more required attrs
    // public AudioSource shoot noise ...

    public float bulletspeed;
    public Transform rotation; // rotation of the gun
    public GameObject image;

    public float scatterCoefficient;
    public float scatterFactor;
    public float scatterOffset;
    public float scatterStart;
    public float scatterEnd;

    private Vector3 rightVector;

    private void Start()
    {
        scatterOffset = 0;
    }

    public void updateRotation()
    {
        rightVector = rotation.forward * -1;
    }

    void Update()
    {
        image.transform.rotation = Quaternion.Euler(image.transform.rotation.eulerAngles.x, image.transform.rotation.eulerAngles.y, rotation.rotation.eulerAngles.z);

        if (scatterOffset < scatterEnd)
        {
            scatterOffset += (scatterFactor / 100) * scatterCoefficient * Time.deltaTime;
        }

        transform.localPosition += rightVector * bulletspeed * Time.deltaTime + new Vector3(scatterOffset, 0, 0);
    }
}
