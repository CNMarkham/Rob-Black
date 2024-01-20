using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health;
    public string name;
    public int damage;
    public float knockbackMultiplier;
    Vector3 RecoilDestination;
    public bool isRecoiling;
    public void Damage(int amount) {

        health -= amount;

    }

    public void Heal(int amount)
    {
        health += amount;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
