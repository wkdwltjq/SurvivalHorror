using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GlobalHealth : MonoBehaviour
{
    public Slider HpSlider;
    public Text HpText;
    public int currentHealth = 100;

    void Update()
    {
        HpText.text = string.Format("{0}", currentHealth);
        HpSlider.value = (float)currentHealth / (float)100;
        if (currentHealth <= 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        this.GetComponent<FirstPersonController>().MouseLookOff();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(6);//게임오버
    }
}

