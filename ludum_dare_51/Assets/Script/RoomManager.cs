using System;
using System.Runtime.ExceptionServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Room> rooms = new List<Room>();
    private Transform spawnPoint;
    private int lastRoom = -1;
    private Room currentRoom;
    private GameObject player;
    
    void Start()
    {
        LoadNewRoom();
        player = GameObject.FindWithTag("Player");
    }

    void LoadNewRoom()
    {
        int indexRoom = -1;
        do {

            indexRoom = Mathf.RoundToInt(UnityEngine.Random.Range(0, rooms.Count));

        } while (lastRoom == indexRoom || indexRoom < 0);
        if (indexRoom != -1)
        {
            currentRoom = Instantiate(rooms[indexRoom], Vector3.zero, Quaternion.identity);
            spawnPoint = currentRoom.transform.Find("SpawnPlayer");
            lastRoom = indexRoom;
        }
        
    }

    void DeleteCurrentRoom()
    {
        currentRoom.DeleteRoom();
    }

    public void ChangeRoom()
    {
        DeleteCurrentRoom();
        LoadNewRoom();
        if (player != null)
        {
            player.transform.position = spawnPoint.position;
        }
    }
}
