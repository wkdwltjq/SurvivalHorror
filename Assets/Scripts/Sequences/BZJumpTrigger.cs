using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BZJumpTrigger : MonoBehaviour
{
    public AudioSource DoorBang;
    public AudioSource DoorJumpMusic;
    public GameObject TheZombie;
    public GameObject TheDoor;
    public GameObject TheJumpTrigger;
    public AudioSource AmbMusic;

    void OnTriggerEnter()
    {
        if (TheJumpTrigger.gameObject.activeSelf == true)
        {
            GetComponent<BoxCollider>().enabled = false;
            TheDoor.GetComponent<Animation>().Play("JumpDoorAnim");
            DoorBang.Play();
            TheZombie.SetActive(true);
            StartCoroutine(PlayJumpMusic());
        }
    }

    IEnumerator PlayJumpMusic()
    {
        yield return new WaitForSeconds(0.4f);
        AmbMusic.Stop();
        DoorJumpMusic.Play();
    }
}
