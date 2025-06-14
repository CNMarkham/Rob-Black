using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaAI : BasicEnemy
{
    // moves in the direction of the player and gets damages if touching bullet

    public DamageManager dm;

    public GameObject Player;
    public float speed;
    public bool touchingPlayer;

    public bool stopLookingAt;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMove>().gameObject;

        if (speed == 0f) { return; }

        speed = Random.Range(speed, speed + speed/2.5f);

        health = (int)((float)health * (index.idx.floornumtodifffloat(PlayerFloorCount.floorNumber)));
        damage = (int)((float)damage * (index.idx.floornumtodifffloat(PlayerFloorCount.floorNumber)) + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = true;
            return;
        }
        if (!other.CompareTag("Bullet") || !other.GetComponent<SimpleBullet>() || dm.emepos == null)
        {
            return;
        }

        var damage = other.GetComponent<SimpleBullet>().damage;

        dm.emepos = other.transform.position;

        dm.addHealth(-damage);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.gameObject == null ) { return; };
        if (!!isRecoiling) { return; }

        if (!stopLookingAt) transform.LookAt(Player.transform);

        var forward = Player.transform.position - this.transform.position;

        if (!touchingPlayer && rb != null)
        {
            rb.velocity += new Vector3(forward.x, 0, forward.z) * speed * Time.deltaTime;
            //Vector3.ClampMagnitude(rb.velocity, speed);
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speed, speed), 0, Mathf.Clamp(rb.velocity.z, -speed, speed));
        }
    }
}

