using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class NextSceneTrig : MonoBehaviour
{
    public GameObject Player;
    PlayerData playerData;

    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerData>();    
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1);
        Player.GetComponent<FirstPersonController>().PlayerMoveStart();
        playerData.SavePlayer();
        SceneManager.LoadScene(5);//Scene003
    }
}
