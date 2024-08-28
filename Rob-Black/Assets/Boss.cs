using System.Collections;
using System.Collections.Generic;
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

    public DamageManager DM;
    public PiranhaAI PA;

    public bossuiscript bui;

    public float cooldown;

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
                print(movespeedold);
                //PA.speed = 0;
                //this line would make it so that the boss stops moving which may be nice but without attacks it would render the boss trivial
                yield return new WaitForSeconds(parameter_input);
                PA.speed = movespeedold;
                print(movespeedold);
                break;

            case Attack.BulletCircle:
                // ADD DELAY BETWEEN EACH Bullet so tgat it makes a spiral to make it easier to esccape

                break;

            case Attack.BulletShotgun:



                break;

            case Attack.Spin:

                PA.stopLookingAt = true;
                Debug.LogWarning("Start Looking");
                for (int i = 0; i < 360 * 1; i++)
                {
                    transform.rotation = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y + 5,
                        transform.rotation.eulerAngles.z
                     );

                    Debug.LogWarning("Spinning");
                    yield return new WaitForFixedUpdate();
                }

                float timecount = 0.00f;

                for (int i = 0; i < 100; i++)
                {
                    timecount += 0.01f;

                    Quaternion q = Quaternion.LookRotation(index.idx.Player.transform.position - transform.position, Vector3.up);
                    //q = Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z);

                    transform.rotation = Quaternion.Slerp(transform.rotation, q, timecount);
                    Debug.LogWarning("Returning To OG");
                    yield return new WaitForFixedUpdate();
                }

                Debug.LogWarning("Stop Looking");
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

        AttackPattern atkptr = index.idx.randomChoice(attkPatts);

        for (int i = 0; i < atkptr.attackSequence.Length; i++)
        {
            print("AttackSequence" + i);
            yield return StartCoroutine(RunAttack(atkptr.attackSequence[i], atkptr.parameters[i]));
            yield return new WaitForSeconds(cooldown);
        }

        StartCoroutine(AttackSequencer());
    }

    // Start is called before the first frame update
    void Start()
    {
        bui = index.idx.bossuiscript;
        StartCoroutine(AttackSequencer());
    }

    // Update is called once per frame
    void Update()
    {
        bui.bossHealthOriginal = DM.maxHealth;
        bui.bossHealth = DM.health;
    }
}
