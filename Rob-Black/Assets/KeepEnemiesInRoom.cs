using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepEnemiesInRoom : MonoBehaviour
{
    public List<GameObject> Enemies;

    public BoxCollider boxCollider;
    public GameObject boxColliderComponentHolder;

    // Update is called once per frame
    void Update()
    {
        Enemies = new();
        float xbounds = (boxCollider.size.x * boxColliderComponentHolder.transform.localScale.x) / 2;
        float ybounds = (boxCollider.size.y * boxColliderComponentHolder.transform.localScale.y) / 2;

        foreach (DamageManager dm in transform.parent.GetComponentsInChildren<DamageManager>())
        {
            Enemies.Add(dm.gameObject);
        }

        foreach (GameObject enemy in Enemies)
        {
          

            enemy.transform.localPosition = new Vector3(

                Mathf.Clamp(enemy.transform.localPosition.x, -xbounds, xbounds), 
                0, Mathf.Clamp(enemy.transform.localPosition.z, -ybounds, ybounds)
                );
        }
    }
}
