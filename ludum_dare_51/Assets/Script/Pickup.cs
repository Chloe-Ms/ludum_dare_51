using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = player.GetComponent<Inventory>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("PlayerTransform"))
        {
            inventory.AddItem(gameObject);
            gameObject.SetActive(false);
        }
    }
}
