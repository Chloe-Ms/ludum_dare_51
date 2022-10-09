using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public RoomManager roomManager;
    public GameObject enemy;
    public Animator animator;
    
    public float health = 100f;

    public bool isdead = false;
    public bool isDying = false;

    private void Start()
    {
        enemy = gameObject;
        roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            StartCoroutine(WaitDie());
        }
    }

    public void Die()
    {
        isdead = true;
        roomManager.RemoveEnemy(gameObject);
        Player_Weapons CurrentCharges = GameObject.Find("Player").GetComponent<Player_Weapons>();
        if (CurrentCharges.CurrentCharge < 3)
        {
            CurrentCharges.CurrentCharge += 1;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage(50);
            Destroy(collision.gameObject);
        }
    }
    
    IEnumerator WaitDie()
    {
        if (animator != null)
        {
            animator.SetTrigger("IsDead");
            
        }
        isDying = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.mass = 1000;
        rb.velocity = new Vector2(0f, 0f);
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        if (capsule != null)
        {
            capsule.enabled = false;
        }

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            box.enabled = false;
        }
        yield return new WaitForSeconds(1f);
        Die();
        
    }
}

