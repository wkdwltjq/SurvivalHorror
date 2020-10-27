using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup2 : MonoBehaviour
{
    public GameObject ammoDisplayBox;
    public GameObject GuideArrow1;
    public GameObject GuideArrow2;
    GlobalAmmo globalAmmo;

    void Start()
    {
        globalAmmo = GameObject.Find("AmmoControl").GetComponent<GlobalAmmo>();
    }

    void OnTriggerEnter(Collider other)
    {
        ammoDisplayBox.SetActive(true);
        globalAmmo.ammoCount += 40;
        GuideArrow1.SetActive(false);
        GuideArrow2.SetActive(true);
        gameObject.SetActive(false);
    }
}
