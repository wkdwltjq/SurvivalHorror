using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpPistol : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject FakePistol;
    public GameObject RealPistol;
    public GameObject ExtraCross;
    public GameObject GuideArror;
    public GameObject GuideArror2;
    public bool PistolCollision = false;
    PlayerCasting playerCasting;

    void Start()
    {
        playerCasting = GameObject.Find("Player").GetComponent<PlayerCasting>();    
    }

    void Update()
    {
        TheDistance = playerCasting.DistanceFromTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        PistolCollision = true;    
    }

    void OnTriggerExit(Collider other)
    {
        PistolCollision = false;    
    }

    void OnMouseOver()
    {
        if (TheDistance <= 2)
        {
            if (PistolCollision == true)
            {
                ExtraCross.SetActive(true);
                ActionText.GetComponent<Text>().text = "Pick Up Pistol";
                ActionDisplay.SetActive(true);
                ActionText.SetActive(true);
            }
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
                FakePistol.SetActive(false);
                RealPistol.SetActive(true);
                ExtraCross.SetActive(false);
                GuideArror.SetActive(false);
                GuideArror2.SetActive(true);
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
