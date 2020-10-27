using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitSound : MonoBehaviour
{
    public AudioSource hurtSound1;
    public AudioSource hurtSound2;
    public AudioSource hurtSound3;
    public int hurtGen;

    public void HitSound()
    {
        hurtGen = Random.Range(1, 4);
        if (hurtGen == 1)
        {
            hurtSound1.Play();
        }
        if (hurtGen == 2)
        {
            hurtSound2.Play();
        }
        if (hurtGen == 3)
        {
            hurtSound3.Play();
        }
    }
}
