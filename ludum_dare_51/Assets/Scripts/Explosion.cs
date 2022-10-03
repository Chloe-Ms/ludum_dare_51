using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Explosion : MonoBehaviour
{

    public int Door = 42; // 42 is the answer to life, the universe and everything



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Player_Life>().Damage(1);
        }
        if (collision.gameObject.layer == 3 )
        {
            Destroy(collision.gameObject);

        }

    }
    /*void OnCollisionStay2D(Collision2D other)
    {
            Debug.Log("DoorDeezNutz");

        if (other.gameObject.layer == 3 )
        {
            Destroy(other.gameObject);
            
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        //Door = Door;
      
    }
}
