using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float knockbackMultiplier;
    Vector3 RecoilDestination;
    public bool isRecoiling;



    public void Damage(int amount) // Damages enemy untill health <= 0 in which case it dies
    {

        health -= amount;

        if (health <= 0)
        {
            Die();
        }

    }

    public void Heal(int amount) // heals player by ammount
    {
        health += amount;
    }

    public void Die() // deletes self + rujns every on death event stored in the enemy
    {
        EnemyDeathEvent[] deathEvents = gameObject.GetComponents<EnemyDeathEvent>();
        // Debug.LogError(deathEvents[0]);
        foreach (EnemyDeathEvent e in deathEvents)
        {
            e.OnDeath(gameObject);
        }

        
        Destroy(gameObject);
    }
}
