using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public int damage;
    public int knockback;

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

    public void rotateDegrees(float n) // It rotates the bullet by n dg
    {
        offsetDegrees += n;
        updateRotation();
    }

    public void updateRotation() // sets the rotation to a new rotation based on quaternion.Euler
    {
        Quaternion oldrot = rotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + offsetDegrees, rotation.eulerAngles.z);

        rightVector = forward * -1;
        transform.rotation = Quaternion.Euler(image.transform.rotation.eulerAngles.x, rotation.eulerAngles.y + 90, image.transform.rotation.eulerAngles.z);

        rotation = oldrot;
    }

    public IEnumerator decayBullet() // makes the bullet fade out after time as to not explode the computer when excess bullets are fired
    {
        float sign = -1f;

        try
        {
            sign = mathindex.randomSign();
        }
        
        catch { }

        float time_between_iterations = decayTime / decayIterations;
        float scatter_degree_per_iteration = sign * (decayEndDegrees / decayIterations); // Get the scatter degree per iteration
        int end_game_iterations = (int)(Mathf.Round(decayIterations / 2)); // Get the end game iterations
        float color_amount_per_iteration = 255 / end_game_iterations;

        SpriteRenderer renderer = image.GetComponent<SpriteRenderer>();

        rotateDegrees(decayStartDegrees * sign); // Choses randomly in which direction the bullet should rotate as it decays

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
        transform.localPosition += transform.right * bulletspeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(image);
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
