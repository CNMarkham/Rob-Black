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
                emepos = pir.transform.position;
                addHealth(-pir.damage);
            }
        }
    }

}
