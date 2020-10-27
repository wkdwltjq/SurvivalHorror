using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState _enemyState;

    //인지 범위
    public float findDistance = 8.0f;
    Transform player;

    //공격범위
    public float attackDistance = 2f;
    public float moveSpeed = 5.0f;

    //공격상태에 사용할 것
    float currentTime = 0;
    float attackDelay = 2.0f;

    public int attackPower = 10;

    //원 위치 저장용 변수
    Vector3 originPos;      //원 위치 벡터
    Quaternion originRot;   //원 위치 회전

    public float moveDistance = 20f;

    //적 체력
    int hp = 20;
    int maxHP = 20;

    CharacterController cc;

    Animator anim;

    NavMeshAgent nav;

    GlobalHealth globalHealth;
    PlayerHitSound playerHitSound;

    public AudioSource ZombieAttackSound;
    public AudioSource ZombieHitSound;

    void Start()
    {
        _enemyState = EnemyState.Idle;
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
        switch (_enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                break;
        }
    }

    //기본 상태
    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            _enemyState = EnemyState.Move;

            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            _enemyState = EnemyState.Return;
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
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
            _enemyState = EnemyState.Attack;
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
            _enemyState = EnemyState.Move;

            currentTime = 0;

            anim.SetTrigger("AttackToMove");
        }
    }

    public void AttackAction()
    {
        globalHealth.currentHealth -= attackPower;
        playerHitSound.HitSound();
    }

    void Return()
    {
        //맨 처음 배치되어 있는 위치에서 거리가 0.1 이상이면 원위치 한다
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            nav.destination = originPos;
            nav.stoppingDistance = 0;
        }
        else
        {
            nav.isStopped = true;
            nav.ResetPath();

            //HP도 회복시켜준다
            transform.position = originPos;
            transform.rotation = originRot;

            _enemyState = EnemyState.Idle;
            anim.SetTrigger("MoveToIdle");

        }
    }
    //코루틴 리턴 데이터
    //yield return null -> 다음 프레임까지 대기한다
    //yield return new WaitForSeconds(float) 지정된 시간동안 대기
    //yield return new WaitForFixedUpdate() 다음 물리프레임까지 대기
    //yield return new WaitForEndofFrame() 모든 렌더링 끝날 때까지 대기
    //yield return StartCoroutine(string) 특정 코루틴 함수 끝날때까지 대기
    //yield return new WWW(string) 웹 통신 작업 끝날때까지 대기
    //yield return new AsyncOperation 비동기 씬 로드가 끝날때까지 대기

    public void HitEnemy(int damage)
    {
        //만약에 내가 맞고 있거나 또는 죽어 있거나 복귀 중이면 함수를 실행하지 않음
        if (_enemyState == EnemyState.Damaged || _enemyState == EnemyState.Die ||
            _enemyState == EnemyState.Return)
        {
            return;
        }


        hp -= damage;

        nav.isStopped = true;
        nav.ResetPath();

        if (hp > 0)
        {
            _enemyState = EnemyState.Damaged;

            anim.SetTrigger("Damaged");
            Damaged();
        }
        else
        {
            _enemyState = EnemyState.Die;
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

        _enemyState = EnemyState.Move;
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

        //Destroy(gameObject);
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<EnemyFSM>().enabled = false;
    }
}
