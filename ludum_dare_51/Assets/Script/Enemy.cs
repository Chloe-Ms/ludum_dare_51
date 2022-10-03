using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject enemy;
    private RoomManager roomManager;
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

    void Die()
    {
        Destroy(gameObject);
        roomManager.RemoveEnemy(enemy);
        Player_Weapons CurrentCharges = GameObject.Find("Square").GetComponent<Player_Weapons>();
        CurrentCharges.CurrentCharge += 1;
    }
}

