using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public int damage;
    public int knockback;

    // any more required attrs
    // public AudioSource shoot noise ...

    // Homing Rocket Launcher
    // The rocket follows the mouse

    public float bulletspeed;
    public Quaternion rotation; // rotation of the gun
    public Vector3 forward;
    public Vector3 right;
    public GameObject image;

    public float decayTime;
    public float decayStartDegrees;
    public float decayEndDegrees;
    public float decayIterations;
    public Color32 bulletBurnColor;

    private Vector3 rightVector;

    public float offsetDegrees;

    public bool DECAY;

    private void Start()
    {

        if (!DECAY) { decayTime = 0; decayStartDegrees = 0; decayEndDegrees = 0; }

        if (DECAY)
        {
            StartCoroutine(decayBullet());
        }
    }

    public void rotateDegrees(float n)
    {
        offsetDegrees += n;
        updateRotation();
    }

    public void updateRotation()
    {
        Quaternion oldrot = rotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + offsetDegrees, rotation.eulerAngles.z);

        rightVector = forward * -1;
        transform.rotation = Quaternion.Euler(image.transform.rotation.eulerAngles.x, rotation.eulerAngles.y + 90, image.transform.rotation.eulerAngles.z);

        rotation = oldrot;
    }

    public IEnumerator decayBullet()
    {

        float sign = index.idx.randomSign();

        float time_between_iterations = decayTime / decayIterations;
        float scatter_degree_per_iteration = sign * (decayEndDegrees / decayIterations); // Get the scatter degree per iteration
        int end_game_iterations = (int)(Mathf.Round(decayIterations / 2)); // Get the end game iterations
        float color_amount_per_iteration = 255 / end_game_iterations;

        SpriteRenderer renderer = image.GetComponent<SpriteRenderer>();

        rotateDegrees(decayStartDegrees * sign);

        for (int i = 0; i < (decayIterations - end_game_iterations); i++)
        {
            rotateDegrees(scatter_degree_per_iteration);
            yield return new WaitForSeconds(time_between_iterations);
        }

        for (int i = 0; i < end_game_iterations; i++)
        {
            renderer.color = new Color32(bulletBurnColor.r, bulletBurnColor.g, bulletBurnColor.b, (byte)(255 - (i * color_amount_per_iteration)));
            rotateDegrees(scatter_degree_per_iteration);
            yield return new WaitForSeconds(time_between_iterations);
        }

        Destroy(image);
        Destroy(gameObject);
        Destroy(this);
    }



    void Update()
    {
        // image.transform.rotation = Quaternion.Euler(image.transform.rotation.eulerAngles.x, image.transform.rotation.eulerAngles.y, rotation.rotation.eulerAngles.z);
        
        transform.localPosition += transform.right * bulletspeed * Time.deltaTime;
    }
}
