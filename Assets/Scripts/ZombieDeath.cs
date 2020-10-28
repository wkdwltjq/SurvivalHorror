using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDeath : MonoBehaviour
{
    public GameObject TheEnemy;
    public AudioSource JumpscareMusic;
    public AudioSource AmbMusic;
    public int EnemyHealth = 20;
    public int StatusCheck;

    void DamageZombie(int DamageAmount)
    {
        EnemyHealth -= DamageAmount;
    }

    void Update()
    {
        if (EnemyHealth <= 0 && StatusCheck == 0)
        {
            this.GetComponent<NavMeshAgent>().speed = 0;
            this.GetComponent<ZombieAi>().enabled = false;
            this.GetComponent<CharacterController>().enabled = false;
            StatusCheck = 2;
            TheEnemy.GetComponent<Animation>().Stop("walk");
            TheEnemy.GetComponent<Animation>().Play("back_fall");
            JumpscareMusic.Stop();
            AmbMusic.Play();
        }
    }
}
