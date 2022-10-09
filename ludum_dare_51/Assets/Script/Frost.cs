using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 4f;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerTransform")
        {
            float speed = collider.gameObject.transform.parent.gameObject.GetComponent<Player_move>().moveSpeed;
            collider.gameObject.transform.parent.gameObject.GetComponent<Player_move>().SetSpeed(speedMultiplier);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerTransform")
        {
            float speed = collider.gameObject.transform.parent.gameObject.GetComponent<Player_move>().moveSpeed;
            collider.gameObject.transform.parent.gameObject.GetComponent<Player_move>().SetSpeed(-speedMultiplier);
        }
    }
}
