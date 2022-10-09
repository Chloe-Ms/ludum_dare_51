using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    public int vie = 1;
    public int vieMax = 1;
    public bool isDead;
    public bool canTakeDamage = true;
    private Animator anim;
    public bool inAnimationDead = false;

    void Start()
    {
        vie = vieMax;
        isDead = false;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }
    public void Damage(int damage)
    {
        vie -= damage;
        if (vie <= 0)
        {
            inAnimationDead = true;
            //On joue l'animation de mort
            anim.SetBool("IsDead",true);
            //Aucun ennemi ne peut le pousser
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.mass = 1000;
            rb.velocity = new Vector2(0f, 0f);
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(StartEndScene());
        }
    }

    IEnumerator StartEndScene()
    {
        yield return new WaitForSeconds(1f);
        isDead = true;
    }

    void OnCollisionEnter2D(Collision2D truc)
    {
        if (truc.gameObject.tag == "Piege")
        {
            Damage(1);
       
        }
    }
}