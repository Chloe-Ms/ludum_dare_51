using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Room> rooms = new List<Room>();
    private int lastRoom = -1;
    private Room currentRoom;
    
    void Start()
    {
        LoadNewRoom();
    }

    void LoadNewRoom()
    {
        int indexRoom = -1;
        do {

            indexRoom = Mathf.RoundToInt(UnityEngine.Random.Range(0, rooms.Count));
        } while (lastRoom == indexRoom && indexRoom < 0);
        if (indexRoom != -1)
        {
            currentRoom = Instantiate(rooms[indexRoom], Vector3.zero, Quaternion.identity);
            lastRoom = indexRoom;
        }
    }



    void DeleteCurrentRoom()
    {
        currentRoom.DeleteRoom();
    }

    void ChangeRoom()
    {
        DeleteCurrentRoom();
        LoadNewRoom();
    }

    void Update()
    {

    }
}
