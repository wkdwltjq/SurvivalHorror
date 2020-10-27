using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearExit : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GameClearSceneExit());    
    }

    IEnumerator GameClearSceneExit()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);//메인메뉴
    }
}
