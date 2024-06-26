using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaAI : BasicEnemy
{
    // moves in the direction of the playtrer snf gets damages if touching bullet

    public DamageManager dm;

    public GameObject Player;
    public float speed;
    public bool touchingPlayer;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMove>().gameObject;

        speed = Random.Range(speed, speed + speed/2.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = true;
        }

        if (other.CompareTag("Bullet"))
        {
            var damage = other.GetComponent<SimpleBullet>().damage;

            dm.emepos = other.transform.position;
            
            dm.addHealth(-damage);
        }
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

        transform.LookAt(Player.transform);


        if (!touchingPlayer)
        {
            // transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            rb.velocity += new Vector3(transform.forward.x, 0, transform.forward.z) * speed * Time.deltaTime;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speed, speed), 0, Mathf.Clamp(rb.velocity.z, -speed, speed));
        }
    }
}

