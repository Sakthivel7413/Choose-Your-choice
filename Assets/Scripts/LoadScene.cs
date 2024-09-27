using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string PlayScene;

    public void LoadScreen()
    {
        SceneManager.LoadScene(PlayScene);
    }
}
