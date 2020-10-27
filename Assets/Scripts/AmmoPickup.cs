using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public GameObject ammoDisplayBox;
    public GameObject GuideArrow;
    GlobalAmmo globalAmmo;

    void Start()
    {
        globalAmmo = GameObject.Find("AmmoControl").GetComponent<GlobalAmmo>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        ammoDisplayBox.SetActive(true);
        globalAmmo.ammoCount += 20;
        GuideArrow.SetActive(false);
        gameObject.SetActive(false);
    }
}
