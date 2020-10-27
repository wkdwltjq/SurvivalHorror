using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftEyePickup : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ExtraCross;
    public GameObject GuideArrow3;
    PlayerCasting playerCasting;
    GlobalInventory globalInventory;

    void Start()
    {
        playerCasting = GameObject.Find("Player").GetComponent<PlayerCasting>();
        globalInventory = GameObject.Find("GlobalInventory").GetComponent<GlobalInventory>();    
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
            ActionText.GetComponent<Text>().text = "Pick Up Left Eye";
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
                GuideArrow3.SetActive(false);
                this.GetComponent<BoxCollider>().enabled = false;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);
                this.gameObject.SetActive(false);
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
