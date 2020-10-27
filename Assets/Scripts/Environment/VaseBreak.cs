using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseBreak : MonoBehaviour
{
    public GameObject fakeVase;
    public GameObject brokenVase;
    public GameObject sphereObject;
    public GameObject KeyObject;
    public GameObject KeyTrigger;
    public AudioSource potteryBreak;

    void DamageZombie(int DamageAmount)
    {
        StartCoroutine(BreakVase());
    }

    IEnumerator BreakVase()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        potteryBreak.Play();
        KeyObject.SetActive(true);
        KeyTrigger.SetActive(true);
        fakeVase.SetActive(false);
        brokenVase.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        sphereObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        sphereObject.SetActive(false);
    }
}
