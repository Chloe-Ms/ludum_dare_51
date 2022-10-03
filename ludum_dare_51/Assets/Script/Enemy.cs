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
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        roomManager.RemoveEnemy(enemy);
        Destroy(gameObject);
        Player_Weapons CurrentCharges = GameObject.Find("Square").GetComponent<Player_Weapons>();
        CurrentCharges.CurrentCharge += 1;
    }
}

