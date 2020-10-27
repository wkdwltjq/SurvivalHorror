using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject theEnemy;
    public GameObject theFlash;
    public float attackDistance;
    public bool attackTrigger = false;
    public bool isAttacking = false;
    NavMeshAgent nav;
    GlobalHealth globalHealth;
    PlayerHitSound playerHitSound;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        attackDistance = 2f;
        globalHealth = GameObject.Find("Player").GetComponent<GlobalHealth>();
        playerHitSound = GameObject.Find("Player").GetComponent<PlayerHitSound>();
    }

    void Update()
    {
        attack();

        if (attackTrigger == false)
        {
            nav.destination = thePlayer.transform.position;
            theEnemy.GetComponent<Animation>().CrossFade("walk", 1f);
        }

        if (attackTrigger == true && isAttacking == false)
        {
            //추적 멈춘다
            nav.isStopped = true;
            //경로 재설정
            nav.ResetPath();

            nav.stoppingDistance = attackDistance;

            theEnemy.GetComponent<Animation>().Play("attack");
            StartCoroutine(InflactDamage());
        }
    }

    void attack()
    {
        if (Vector3.Distance(theEnemy.transform.position, thePlayer.transform.position) < attackDistance)
        {
            attackTrigger = true;
        }
        else if (isAttacking == false)
        {
            attackTrigger = false;
        }
    }

    IEnumerator InflactDamage()
    {
        isAttacking = true;
        playerHitSound.HitSound();
        theFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        theFlash.SetActive(false);
        globalHealth.currentHealth -= 10;
        yield return new WaitForSeconds(2);
        isAttacking = false;
    }
}
