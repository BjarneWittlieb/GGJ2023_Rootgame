using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnDead : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool endGame = false;
    private void OnApplicationQuit() {
        endGame = true;
    }
    private void OnDestroy() {
        if (endGame)
            return;
        SceneManager.LoadScene(sceneName);
    }
}
