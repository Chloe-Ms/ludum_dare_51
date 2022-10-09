using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Chargement : MonoBehaviour
{
    public string level_1;
    public GameObject panel;
    public Player_Life mort;
    public RoomManager roomManager;
    public Sprite endingSprite;
    public Sprite deathSprite;

    private void Start()
    {
        Time.timeScale = 1f;
        if (panel != null){
            panel.SetActive(false);
        }

    }
    private void Update()
    {
        if (roomManager != null && roomManager.isFinished && panel != null)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            panel.GetComponent<Image>().sprite = endingSprite;

        } 
        /*else if (mort != null && mort.isDead == true && panel != null)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            panel.GetComponent<Image>().sprite = deathSprite;
        }*/
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