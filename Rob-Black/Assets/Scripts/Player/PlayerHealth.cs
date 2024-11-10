using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : DamageManager
{
    private void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("Enemy"))
        {
            var pir = other.GetComponent<BasicEnemy>();


            if (pir)
            {
                emepos = pir.transform.position; // sets the position of the enemy to the current enemy
                addHealth(-pir.damage); // damages player
            }
        }

        if (other.CompareTag("EnemyBullet"))
        {
            var pir = other.GetComponent<SimpleBullet>();

            if (pir)
            {
                emepos = pir.transform.position; // sets the position of the enemy to the current enemy
                addHealth(-pir.damage); // damages player
            }
        }
    }

}
