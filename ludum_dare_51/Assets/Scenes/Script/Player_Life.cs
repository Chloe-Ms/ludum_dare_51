using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    public int vie = 1;
    public int vieMax = 1;
    public bool isDead;

    void Start()
    {
        vie = vieMax;
        isDead = false;
    }
    private void Update()
    {
        
    }
    public void Damage(int damage)
    {
        vie -= damage;
        if (vie <= 0)
        {
            isDead = true;  
        }
    }

    void OnCollisionEnter2D(Collision2D truc)
    {
        if (truc.gameObject.tag == "Piege")
        {
            Damage(1);
       
        }
    }
}