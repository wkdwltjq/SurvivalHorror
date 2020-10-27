using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    GlobalHealth globalHealth;
    GlobalAmmo globalAmmo;

    void Start()
    {
        globalHealth = GameObject.Find("Player").GetComponent<GlobalHealth>();
        globalAmmo = GameObject.Find("AmmoControl").GetComponent<GlobalAmmo>();
        if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            LoadPlayer();
        }
    }

    public void SavePlayer()
    {
        GameData save = new GameData();
        save.HP = globalHealth.currentHealth;
        save.Bullets = globalAmmo.ammoCount;

        DataManager.Save(save);
    }

    public void LoadPlayer()
    {
        GameData save = DataManager.Load();
        globalHealth.currentHealth = save.HP;
        globalAmmo.ammoCount = save.Bullets;
    }
}
