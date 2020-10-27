using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossTrig : MonoBehaviour
{
    public GameObject BossAppearanceCamera;
    public GameObject Player;
    public GameObject Boss;
    public GameObject BossAnim;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(bossAppearance()); 
    }

    IEnumerator bossAppearance()
    {
        Player.SetActive(false);
        BossAppearanceCamera.SetActive(true);
        Boss.SetActive(true);
        yield return new WaitForSeconds(3);
        BossAppearanceCamera.SetActive(false);
        Player.SetActive(true);
        Boss.GetComponent<NavMeshAgent>().enabled = true;
        Boss.GetComponent<BossFSM>().enabled = true;
        BossAnim.GetComponent<Animator>().enabled = true;
        gameObject.SetActive(false);
    }
}
