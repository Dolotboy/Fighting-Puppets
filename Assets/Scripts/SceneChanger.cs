using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSceneBySceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneBySceneNbr(int sceneNbr)
    {
        SceneManager.LoadScene(sceneNbr);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
