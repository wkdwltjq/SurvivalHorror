using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverExit : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MainMenuScene());    
    }

    IEnumerator MainMenuScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);//MainMenuScene
    }
}
