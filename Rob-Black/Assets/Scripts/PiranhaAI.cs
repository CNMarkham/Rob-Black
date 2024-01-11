using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaAI : BasicEnemy
{
    public GameObject Player;
    public float speed;
    public bool touchingPlayer;

    // Start is called before the first frame update
    void startAppend()
    {
        Player = FindObjectOfType<PlayerHealth>().gameObject;
    }

    private void triggerEnterAppend(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = true;
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
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }
}
