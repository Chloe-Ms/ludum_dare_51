using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public int Door = 42; // 42 is the answer to life, the universe and everything


    private void Start()
    {
        Destroy(gameObject, 2f);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Player_Life>().Damage(1);
        }
        if (collision.gameObject.layer == 3 )
        {
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().Die();
            }
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
