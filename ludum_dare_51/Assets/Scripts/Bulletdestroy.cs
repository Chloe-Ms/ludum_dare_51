using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletdestroy : MonoBehaviour
{

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Walls" || (collision.gameObject.tag == "Enemy" && collision.gameObject != enemy)) 
        {
            Destroy(gameObject);
        }
    }

}
