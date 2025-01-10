using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDeathEvent : MonoBehaviour
{
    public abstract void OnDeath(GameObject enemy);

}
