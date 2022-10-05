using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public RoomManager roomManager;
    public GameObject enemy;
    
    public float health = 100f;

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
            Die();
        }
    }

    public void Die()
    {
        roomManager.RemoveEnemy(gameObject);
        Player_Weapons CurrentCharges = GameObject.Find("Player").GetComponent<Player_Weapons>();
        CurrentCharges.CurrentCharge += 1;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Die();
        }
    }
}

