using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health;
    public string name;
    public int damage;
    public float knockbackMultiplier;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            Damage(other.gameObject.GetComponent<SimpleBullet>().damage);

            if (health <= 0) { Die();  }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, other.gameObject.transform.position * -1, knockbackMultiplier);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && (int)Mathf.Floor(Time.time * 1000) % 250 == 0)
        {

            other.gameObject.GetComponentInParent<PlayerHealth>().Damage(damage);

        }
    }
}
