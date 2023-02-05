using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    public void startScene() {
        SceneManager.LoadScene(sceneName);
    }

    public void endGame() {
        Application.Quit();
    }
}
