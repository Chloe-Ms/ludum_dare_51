using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player_Life>().Damage(1);
        }
        if (collider.gameObject.tag == "Enemy"){
            Destroy(collider.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player_Life>().Damage(1);
        }
        if (collider.gameObject.tag == "Enemy"){
            Destroy(collider.gameObject);
        }
    }
}
