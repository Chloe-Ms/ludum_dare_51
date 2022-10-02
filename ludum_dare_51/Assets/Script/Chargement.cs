using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chargement : MonoBehaviour
{
    public string level_1;

    public void load() 
    {
        SceneManager.LoadScene(level_1);
        Time.timeScale = 1f;
    }

    public void quitter()
    {
        Application.Quit();
    }
}