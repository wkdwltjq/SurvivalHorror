using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BreakingGroundTrigger : MonoBehaviour
{
    public GameObject Ground;
    public GameObject Player;
    public AudioSource FallingRookSound;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(BreakingGround());
    }

    IEnumerator BreakingGround()
    {
        FallingRookSound.Play();
        yield return new WaitForSeconds(0.1f);
        Ground.SetActive(false);
        Player.GetComponent<FirstPersonController>().PlayerMoveStop();
    }
}
