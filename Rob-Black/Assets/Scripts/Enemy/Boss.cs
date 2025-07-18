using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Attack
    {
        WaitSeconds, // wait X seconds

        Spin, // spin while attacking for x seconds
     
        BulletCircle, // how many bullets in the circle
        BulletShotgun, // how many bullets should fire from shotgun

        SpeedUp, // speed up % (- makes it slow down %)       
        Heal, // How many points by (rounded)

        BoostDamage // Input is percent increased

    }

    public string bossname;

    public DamageManager DM;
    public PiranhaAI PA;

    public List<GameObject> bossDrops;

    public bossuiscript bui;
    public GameObject basicGun;

    public float cooldown;
    public bool noupdate = false;

    [System.Serializable]
    public struct AttackPattern
    {
        public Attack[] attackSequence;
        public float[] parameters;
    }
    public List<AttackPattern> attkPatts = new List<AttackPattern>();
    IEnumerator RunAttack(Attack attack, float parameter)
    {
        var parameter_input = parameter;

        switch (attack)
        {
            case Attack.WaitSeconds:
                float movespeedold = PA.speed;
                yield return new WaitForSeconds(parameter_input);
                PA.speed = movespeedold;
                break;

            case Attack.BulletCircle:

                // ADD DELAY BETWEEN EACH Bullet so tgat it makes a spiral to make it easier to esccape
                for (int i = 0; i < parameter_input; i++)
                {
                    yield return StartCoroutine(basicGun.GetComponent<BasicGun>().shoot());
                    basicGun.transform.Rotate(new Vector3(0, 360 / parameter_input, 0));
                }

                break;

            case Attack.BulletShotgun:

                basicGun.transform.Rotate(new Vector3(0, 180, 0));
                int oldshot = (int)basicGun.GetComponent<BasicGun>().bulletsPerShot;
                basicGun.GetComponent<BasicGun>().bulletsPerShot = parameter_input;
                yield return StartCoroutine(basicGun.GetComponent<BasicGun>().shoot());
                basicGun.GetComponent<BasicGun>().bulletsPerShot = oldshot;
                basicGun.transform.Rotate(new Vector3(0, -180, 0));

                break;

            case Attack.Spin:

                PA.stopLookingAt = true;
                for (int i = 0; i < 360 * 1; i++)
                {
                    transform.rotation = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y + 5,
                        transform.rotation.eulerAngles.z
                     );

                    yield return new WaitForFixedUpdate();
                }

                float timecount = 0.00f;

                for (int i = 0; i < 100; i++)
                {
                    timecount += 0.01f;

                    Quaternion q = Quaternion.LookRotation(index.idx.Player.transform.position - transform.position, Vector3.up);

                    transform.rotation = Quaternion.Slerp(transform.rotation, q, timecount);

                    yield return new WaitForFixedUpdate();
                }

                PA.stopLookingAt = false;

                break;

            case Attack.SpeedUp:
                PA.speed *= Mathf.Max(1 + (parameter_input / 100), 0);
                break;

            case Attack.Heal:
                PA.health += (int)parameter_input;
                break;

            case Attack.BoostDamage:
                PA.damage = (int)((float)PA.damage * Mathf.Max(1 + (parameter_input / 100), 0));
                break;

            default:
                break;
        }

    }

    

    IEnumerator AttackSequencer()
    {

        AttackPattern atkptr = mathindex.randomChoice(attkPatts);

        for (int i = 0; i < atkptr.attackSequence.Length; i++)
        {
            yield return StartCoroutine(RunAttack(atkptr.attackSequence[i], atkptr.parameters[i]));
            yield return new WaitForSeconds(cooldown);
        }

        StartCoroutine(AttackSequencer());
    }

    // Start is called before the first frame update
    void Start()
    {
        bui = index.idx.bossuiscript;

        PA = GetComponent<PiranhaAI>();
        DM = GetComponent<DamageManager>();

        bui.bossObject = this.gameObject;
        bui.bossHealth = DM.health;
        bui.bossHealthOriginal = DM.maxHealth; 

        StartCoroutine(AttackSequencer());

        BasicGun bg = basicGun.GetComponent<BasicGun>();
        bg.bulletDamage = (int)((float)bg.bulletDamage * (index.idx.floornumtodifffloat(PlayerFloorCount.floorNumber)) + 1) + 1;
    }

    private void OnDestroy()
    {
        noupdate = true;
        bui.bossHealthOriginal = 0;
        bui.bossHealth = 0;
        bui.bossbarvisible = false;

        if (bossDrops.Count != 0)
        {
            index.idx.guntoaparatus(mathindex.randomChoice(bossDrops), 0, transform.position);
        }

        bui.bossObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (noupdate) { return; }

        bui.bossbarvisible = true;

        bui.bossHealthOriginal = DM.maxHealth;
        bui.bossHealth = DM.health;


    }
}
