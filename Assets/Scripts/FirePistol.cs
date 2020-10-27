using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePistol : MonoBehaviour
{
    public GameObject TheGun;
    public GameObject MuzzleFlash;
    public AudioSource GunFire;
    public bool IsFiring = false;
    public float TargetDistance;
    public int DamageAmount = 5;
    GlobalAmmo globalAmmo;

    void Start()
    {
        globalAmmo = GameObject.Find("AmmoControl").GetComponent<GlobalAmmo>();    
    }

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

        if (Input.GetButtonDown("Fire1") && globalAmmo.ammoCount >= 1)
        {
            if (IsFiring == false)
            {
                globalAmmo.ammoCount -= 1;
                StartCoroutine(FiringPistol());
            }
        }
    }

    IEnumerator FiringPistol()
    {
        RaycastHit Shot;
        IsFiring = true;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;
            Shot.transform.SendMessage("DamageZombie", DamageAmount, SendMessageOptions.DontRequireReceiver);
            Shot.transform.SendMessage("HitEnemy", DamageAmount, SendMessageOptions.DontRequireReceiver);
        }
        TheGun.GetComponent<Animation>().Play("PistolShotAnim");
        MuzzleFlash.SetActive(true);
        MuzzleFlash.GetComponent<Animation>().Play("MuzzleAnim");
        GunFire.Play();
        yield return new WaitForSeconds(0.5f);
        MuzzleFlash.SetActive(false);
        IsFiring = false;
    }
}
