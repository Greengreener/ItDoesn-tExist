using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }
    //Changes scene based on index
    public void LoadNewScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    //Quits game
    public void QuitGame()
    {
#if UNITY_EDITOR
//Closes editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}