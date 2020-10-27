using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAmmo : MonoBehaviour
{
    public GameObject ammoDisplay;
    public int ammoCount;

    void Update()
    {
        ammoDisplay.GetComponent<Text>().text = "" + ammoCount;
    }
}
