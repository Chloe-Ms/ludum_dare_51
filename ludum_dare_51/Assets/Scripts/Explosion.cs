using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Explosion : MonoBehaviour
{

    public int Door = 42; // 42 is the answer to life, the universe and everything

    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("deeznuts");
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
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
        Door = Door;
      
    }
}
