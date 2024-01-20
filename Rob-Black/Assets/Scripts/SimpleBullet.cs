using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public int damage;
    public int knockback;

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
    private Vector3 offsetVector;

    public float offsetDegrees;

    public bool SCATTER;

    private void Start()
    {
        scatterOffset = 0;
    }

    public void rotateDegrees(float n)
    {
        offsetDegrees += n;
        updateRotation();
    }

    public void updateRotation()
    {
        Quaternion oldrot = rotation.rotation;
        rotation.rotation = Quaternion.Euler(rotation.rotation.eulerAngles.x, rotation.rotation.eulerAngles.y + offsetDegrees, rotation.rotation.eulerAngles.z);

        rightVector = rotation.forward * -1;
        image.transform.rotation = Quaternion.Euler(image.transform.rotation.eulerAngles.x, rotation.transform.rotation.eulerAngles.y + 90, image.transform.rotation.eulerAngles.z);
        offsetVector = rotation.right;

        rotation.rotation = oldrot;
    }

    void updateScatter()
    {
        scatterOffset += (scatterFactor / 100) * scatterCoefficient * Time.deltaTime;
        scatterCoefficient = Random.Range(scatterCoefficient, scatterCoefficient + 2f) * (Random.Range(0, 1) * 2 - 1);
    }

    void Update()
    {
        // image.transform.rotation = Quaternion.Euler(image.transform.rotation.eulerAngles.x, image.transform.rotation.eulerAngles.y, rotation.rotation.eulerAngles.z);

        if (scatterOffset < scatterEnd && SCATTER)
        {
            updateScatter();
        }
        
        transform.localPosition += rightVector * bulletspeed * Time.deltaTime + scatterOffset * offsetVector;
    }
}
