using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class testasc : ScriptableObject
{

    [Header("Gun Data")]

    public bool disableShooting;
    public string Name;
    public SpriteRenderer spriteRenderer;
    public GameObject Bullet;
    public Color32 bulletBurnColor;

    [Header("Gun Settings")]
    public bool holdDown;

    [Header("Bullet Settings")]

    public float bulletSpeed;
    public Transform bulletStartPos;

    [Header("Reloading")]

    public float magazineSize; // Magazine size
    public float reloadTime; // ms // reload time

    [Header("Shooting")]

    public float bulletsPerShot = 1;
    public float bulletsPerSecond;
    public float bulletSpreadDegree;

    [Header("Decay")]

    // TODO:
    // Scattering -- based on decay time

    public float decayTime;
    public float decayStartDegrees = 0; // KEEP @ 0 FOR MOST OF THE TIME UNLESS YOU WANT A HOLE IN THE MIDDLE
    public float decayEndDegrees;
    public float decayIterations = 100;
    public bool DECAY;

    [Header("Burst")]

    public bool disableBurst;

    // make it so that the values below get greyed out

    public float burstSize;
    public float timeBetweenBursts;

    [Header("Internal")]

    public int shotsFired;
    public bool canShoot;
    public bool isReloading;

}
