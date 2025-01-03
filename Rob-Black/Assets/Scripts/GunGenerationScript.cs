using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGenerationScript : MonoBehaviour
{
    public enum guntype
    {
        pistol,
        assaultrifle,
        russianassaultrifle
    }

    public List<string> ARFishNames = new List<string> {"TRO-UT", "SUN-FISH", "MAR-LIN"};
    public List<string> RARFishNames = new List<string> { "FR-Y", "CAT-FISH", "TILA-P-IA" };
    public List<string> PFishNames = new List<string> { "SAL-MON", "EEL", "LION-FISH" };

    public BasicGun GetBasicGun(guntype type, int seed, int dificulty) {

        System.Random rng = new(seed * dificulty);

        if (type == guntype.assaultrifle)
        {
            // BasicGun bg = Instantiate()
            BasicGun bg = new();
            
            bg.Name = ARFishNames[rng.Next(ARFishNames.Count)] + $" {rng.Next(1, dificulty*5)}";
            bg.spriteRenderer.sprite = index.idx.assaultrifle;
            bg.bulletBurnColor = new Color32(22, 222, 89, 255);

            bg.gunColor = new Color32(255, 255, 255, 255);

            bg.holdDown = true;

            bg.bulletLengthPercentOfStandardBullet = 1; // 1 = it has the same length as the regular bullet prefab; 0.5 half as long, 2 twice as long
            bg.bulletHeightPercentOfStandardBullet = 1; // Height instead of length 

            bg.bulletSpeed = 1;
            bg.bulletDamage = 1;

            bg.magazineCount = 1;
            bg.magazineSize = 1; // Magazine size
            bg.reloadTime = 1; // ms // reload time

            bg.bulletsPerShot = 1;
            bg.bulletsPerSecond = 1;
            bg.bulletSpreadDegree = 1;


            bg.decayTime =1 ;
            bg.decayStartDegrees = 0; // KEEP @ 0 FOR MOST OF THE TIME UNLESS YOU WANT A HOLE IN THE MIDDLE
            bg.decayEndDegrees = 1;
            bg.decayIterations = 100;
            bg.DECAY = true;

            bg.disableBurst = false;
            bg.burstSize = 1 ;
            bg.timeBetweenBursts = 1;

        }

        return null;
    }
}
