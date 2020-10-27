using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookedDoor : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ExtraCross;
    public GameObject firstKeyDoor;
    public AudioSource lookedDoor;
    public AudioSource CreakSound;
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
            ActionText.GetComponent<Text>().text = "Open Door";
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
                ExtraCross.SetActive(false);
                StartCoroutine(DoorReset());
            }
        }
    }

    void OnMouseExit()
    {
        ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }

    IEnumerator DoorReset()
    {
        if (globalInventory.firstDoorKey == false)
        {
            lookedDoor.Play();
            yield return new WaitForSeconds(1);
            this.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            firstKeyDoor.GetComponent<Animation>().Play("FirstKeyDoor");
            CreakSound.Play();
            yield return new WaitForSeconds(2f);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
