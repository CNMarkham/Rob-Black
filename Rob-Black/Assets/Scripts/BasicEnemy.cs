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



    private void Start()
    {
        RecoilDestination = transform.position;
        startAppend();
    }

    private void startAppend() { }

    private void Update()
    {
        if (index.idx.Round(gameObject.transform.position, 2) != index.idx.Round(RecoilDestination,2) && isRecoiling)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, RecoilDestination, 0.05f);
        }

        else
        {
            RecoilDestination = transform.position;
            isRecoiling = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            SimpleBullet sb = other.gameObject.GetComponent<SimpleBullet>();

            if (other.gameObject != null && sb != null)
            {
                Damage(sb.damage);

                Destroy(other.gameObject);

                if (health <= 0) { Die(); }
                RecoilDestination = gameObject.transform.position + other.transform.right * knockbackMultiplier;
                isRecoiling = true;
               // gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position , gameObject.transform.position + other.transform.right *knockbackMultiplier, knockbackMultiplier);
            }

        }

        triggerEnterAppend();
    }

    private void triggerEnterAppend() { }
    private void triggerStayAppend() { }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && (int)Mathf.Floor(Time.time * 1000) % 250 == 0)
        {

            other.gameObject.GetComponentInParent<PlayerHealth>().Damage(damage);

        }

        triggerStayAppend();
    }
}
