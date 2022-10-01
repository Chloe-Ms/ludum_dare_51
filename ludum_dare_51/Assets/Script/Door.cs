using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private RoomManager manager;

    void Start(){
        GameObject go = GameObject.Find("RoomManager");
        manager = (RoomManager)go.GetComponent(typeof(RoomManager));
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            manager.ChangeRoom();
        }
    }
}
