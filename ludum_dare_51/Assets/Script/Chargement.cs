using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;


public class Chargement : MonoBehaviour
{
    public string level_1;
    public GameObject panel;
    private void Start()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            panel.SetActive(true);
        }
    }
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