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

    public void Die() // deletes self + runs every on death event stored in the enemy
    {
        EnemyDeathEvent[] deathEvents = gameObject.GetComponents<EnemyDeathEvent>();
        
        foreach (EnemyDeathEvent e in deathEvents)
        {
            e.OnDeath(gameObject);
        }

        
        Destroy(gameObject);
    }
}
