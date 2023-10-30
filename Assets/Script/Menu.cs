using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Scenes(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }

    public void Exid()
    {
        Application.Quit();
    }
}
