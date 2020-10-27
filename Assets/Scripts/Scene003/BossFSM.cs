using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class BossFSM : MonoBehaviour
{
    enum BossState
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die
    }

    BossState _bossState;

    Transform player;

    //공격범위
    public float attackDistance = 3f;
    public float MoveSpeed = 10f;

    //공격상태에 사용할 것
    float currentTime = 0;
    float attackDelay = 2.0f;

    public int attackPower = 30;

    //원 위치 저장용 변수
    Vector3 originPos;      //원 위치 벡터
    Quaternion originRot;   //원 위치 회전

    //적 체력
    int hp = 40;
    int maxHP = 40;

    public Slider hpSlider;

    CharacterController cc;

    Animator anim;

    NavMeshAgent nav;

    GlobalHealth globalHealth;
    PlayerHitSound playerHitSound;

    public AudioSource ZombieAttackSound;
    public AudioSource ZombieHitSound;
    public GameObject Player;

    void Start()
    {
        _bossState = BossState.Move;
        player = GameObject.Find("Player").transform;

        originPos = transform.position;
        originRot = transform.rotation;

        cc = GetComponent<CharacterController>();

        anim = transform.GetComponentInChildren<Animator>();

        nav = GetComponent<NavMeshAgent>();

        globalHealth = GameObject.Find("Player").GetComponent<GlobalHealth>();
        playerHitSound = GameObject.Find("Player").GetComponent<PlayerHitSound>();
    }

    void Update()
    {
        switch (_bossState)
        {
            case BossState.Idle:
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Damaged:
                break;
            case BossState.Die:
                break;
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            //추적 멈춘다
            nav.isStopped = true;
            //경로 재설정
            nav.ResetPath();

            nav.stoppingDistance = attackDistance;
            nav.destination = player.position;
        }
        else
        {
            _bossState = BossState.Attack;
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                currentTime = 0;
                anim.SetTrigger("StartAttack");
                ZombieAttackSound.Play();
            }
        }
        else
        {
            _bossState = BossState.Move;

            currentTime = 0;

            anim.SetTrigger("AttackToMove");
        }
    }

    public void AttackAction()
    {
        globalHealth.currentHealth -= attackPower;
        playerHitSound.HitSound();
    }

    public void HitEnemy(int damage)
    {
        //만약에 내가 맞고 있거나 또는 죽어 있거나 복귀 중이면 함수를 실행하지 않음
        if (_bossState == BossState.Damaged || _bossState == BossState.Die)
        {
            return;
        }


        hp -= damage;

        nav.isStopped = true;
        nav.ResetPath();

        if (hp > 0)
        {
            _bossState = BossState.Damaged;

            anim.SetTrigger("Damaged");
            Damaged();
        }
        else
        {
            _bossState = BossState.Die;
            anim.SetTrigger("Die");
            Die();
        }

    }

    void Damaged()
    {
        StartCoroutine(DamagedProcess());
    }

    IEnumerator DamagedProcess()
    {
        ZombieHitSound.Play();
        //피격 모션 시간을 기다리게 명령해준다
        yield return new WaitForSeconds(1.0f);

        _bossState = BossState.Move;
    }

    void Die()
    {
        //진행 중일 수 있는 피격 모션 코루틴을 중지하고
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);

        //hpSlider.gameObject.SetActive(false);
        //this.GetComponent<NavMeshAgent>().enabled = false;
        //this.GetComponent<CharacterController>().enabled = false;
        //this.GetComponent<BossFSM>().enabled = false;
        Player.GetComponent<FirstPersonController>().MouseLookOff();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(7);//GameClear씬
    }
}
