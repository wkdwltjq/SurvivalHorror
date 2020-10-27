using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorJump : MonoBehaviour
{
    public GameObject sphereTrig;

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        sphereTrig.SetActive(true);
        StartCoroutine(DeactivateSphere());
    }

    IEnumerator DeactivateSphere()
    {
        yield return new WaitForSeconds(0.5f);
        sphereTrig.SetActive(false);
    }
}
