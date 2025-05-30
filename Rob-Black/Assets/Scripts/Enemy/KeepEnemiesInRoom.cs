using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepEnemiesInRoom : MonoBehaviour
{
    public List<GameObject> Enemies;

    public BoxCollider boxCollider;
    public GameObject boxColliderComponentHolder;

    public static void keepEntityInRoom(GameObject entity, BoxCollider bc, GameObject box, float roomScale = 1.0f)
    {
        float xbounds = ((bc.size.x * box.transform.localScale.x) / 2) * roomScale;
        float ybounds = ((bc.size.y * box.transform.localScale.y) / 2) * roomScale;

        entity.transform.position = new Vector3(
            Mathf.Clamp(entity.transform.position.x, -xbounds + box.transform.parent.position.x, xbounds + box.transform.parent.position.x),
            0, Mathf.Clamp(entity.transform.position.z, -ybounds + box.transform.parent.position.z, ybounds + box.transform.parent.position.z) 
        );
    }

    // Update is called once per frame
    void Update()
    {

        Enemies = new();

        foreach (DamageManager dm in transform.parent.GetComponentsInChildren<DamageManager>())
        {
            //Enemies.Add(dm.gameObject);
            keepEntityInRoom(dm.gameObject, boxCollider, boxColliderComponentHolder);
        }
    }
}
