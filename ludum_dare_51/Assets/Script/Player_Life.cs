using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    public int vie = 1;
    [HideInInspector] public int vieMax = 1;
    private Vector2 spawnPoint;


    void Start()
    {
        vie = vieMax;
    }
    void OnCollisionEnter2D(Collision2D truc)
    {
        if (truc.gameObject.tag == "Piege")
        {
            vie = 0;
        }
    }
}