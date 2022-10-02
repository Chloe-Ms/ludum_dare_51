using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 2f;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            float speed = collider.gameObject.GetComponent<Player_move>().moveSpeed;
            collider.gameObject.GetComponent<Player_move>().SetSpeed(speed * speedMultiplier);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            float speed = collider.gameObject.GetComponent<Player_move>().moveSpeed;
            collider.gameObject.GetComponent<Player_move>().SetSpeed(speed / speedMultiplier);
        }
    }
}
