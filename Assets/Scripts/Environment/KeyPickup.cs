using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ExtraCross;
    public GameObject TheKey;
    public GameObject GuideArrow2;
    GlobalInventory globalInventory;
    PlayerCasting playerCasting;

    void Start()
    {
        globalInventory = GameObject.Find("GlobalInventory").GetComponent<GlobalInventory>();
        playerCasting = GameObject.Find("Player").GetComponent<PlayerCasting>();
    }

    void Update()
    {
        TheDistance = playerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance <= 2)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "Pick Up Key";
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
                GuideArrow2.SetActive(false);
                this.GetComponent<BoxCollider>().enabled = false;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);
                TheKey.SetActive(false);
                globalInventory.firstDoorKey = true;
            }
        }
    }

    void OnMouseExit()
    {
        ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }
}
