using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private RoomManager manager;
    public GameObject recompense;
    void Start(){
        GameObject go = GameObject.Find("RoomManager"); //Get the room manager to call its function
        manager = (RoomManager) go.GetComponent(typeof(RoomManager));
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && manager.IsRoomCleared())
        {
            
            manager.ChangeRoom(recompense);
        }
    }
}
