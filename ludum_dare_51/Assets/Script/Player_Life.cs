using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    public int vie = 5;
    [HideInInspector] public int vieMax;
    private Vector2 spawnPoint;


    void Start()
    {
        if (PlayerPrefs.GetInt("playerLIFE") == 0)
        {
            PlayerPrefs.SetInt("playerLIFE", 5);
        }
        vieMax = PlayerPrefs.GetInt("playerLIFE");
        vie = vieMax;
    }

    void Update()
    {
        if (vie <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void takeDamage(int damage)
    {
        vie = vie - damage;
    }

    public void heal(int heal)
    {
        vieMax = PlayerPrefs.GetInt("playerLIFE");
        vie = vie + heal;
        if (vie > vieMax)
        {
            vie = vieMax;
        }
    }

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "checkPoint")
        {
            spawnPoint = truc.transform.position;
        }
    }
    void OnCollisionEnter2D(Collision2D truc)
    {
        if (truc.gameObject.tag == "Piege")
        {
            vie = 0;
        }
    }
}