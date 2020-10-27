using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CellExitDoor : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ExtraCross;
    public GameObject fadeOut;
    public GameObject Zombie;
    PlayerCasting playerCasting;
    PlayerData playerData;

    void Start()
    {
        playerCasting = GameObject.Find("Player").GetComponent<PlayerCasting>();
        playerData = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    void Update()
    {
        TheDistance = playerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (Zombie.activeSelf == true)
        {
            if (TheDistance <= 2)
            {
                ActionText.GetComponent<Text>().text = "Open Door";
                ExtraCross.SetActive(true);
                ActionDisplay.SetActive(true);
                ActionText.SetActive(true);
            }
            else
            {
                ExtraCross.SetActive(false);
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
            }
            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 2)
                {
                    this.GetComponent<BoxCollider>().enabled = false;
                    ActionDisplay.SetActive(false);
                    ActionText.SetActive(false);
                    fadeOut.SetActive(true);
                    StartCoroutine(FadeToExit());
                }
            }
        }
    }

    void OnMouseExit()
    {
        ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }

    IEnumerator FadeToExit()
    {
        yield return new WaitForSeconds(3);
        playerData.SavePlayer();//플레이어 정보 저장
        SceneManager.LoadScene(4); //Scene002
    }
}
