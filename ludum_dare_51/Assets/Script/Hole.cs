using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerTransform" && collider.gameObject.transform.parent.gameObject.GetComponent<Player_move>().canDash)
        {
            //collider.gameObject.transform.parent.gameObject.GetComponent<Player_move>().canDash
            //collider.gameObject.GetComponent<Player_Life>().Damage(1);
            collider.gameObject.transform.parent.gameObject.GetComponent<Player_Life>().Damage(1);
        }
        if (collider.gameObject.tag == "EnemyTransform"){
            GameObject.Find("RoomManager").GetComponent<RoomManager>().RemoveEnemy(collider.gameObject.transform.parent.gameObject);
            Destroy(collider.gameObject.transform.parent.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerTransform" && collider.gameObject.transform.parent.gameObject.GetComponent<Player_move>().canDash)
        {
            collider.gameObject.transform.parent.gameObject.GetComponent<Player_Life>().Damage(1);
        }

        if (collider.gameObject.tag == "EnemyTransform")
        {
            GameObject.Find("RoomManager").GetComponent<RoomManager>().RemoveEnemy(collider.gameObject.transform.parent.gameObject);
            Destroy(collider.gameObject.transform.parent.gameObject);
        }
    }
}
