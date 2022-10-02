using System;
using System.Runtime.ExceptionServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Room> rooms = new List<Room>();
    private int lastRoom = -1;
    private Room currentRoom;
    private GameObject player;
    [SerializeField] private bool roomCleared = true;


    [SerializeField] Tilemap floorTilemap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private int maxHeight = 10;
    [SerializeField] private int minHeight = 5;
    [SerializeField] private int maxWidth = 10;
    [SerializeField] private int minWidth = 5;

    void Start()
    {
        LoadNewRoom();
        player = GameObject.FindWithTag("Player");
    }

    void LoadNewRoom()
    {
        int indexRoom = -1;
        do {

            indexRoom = UnityEngine.Random.Range(0, rooms.Count);

        } while (lastRoom == indexRoom || indexRoom < 0);
        if (indexRoom != -1)
        {
            int width = GetRandomWidth(minWidth,maxWidth);
            int height = GetRandomHeight(minHeight,maxHeight);
            HashSet<Vector2Int> map = CreateRectangle(width, height, Vector2Int.RoundToInt((Vector2)spawnPoint.position));
            PaintFloorTiles(map);
            lastRoom = indexRoom;
        } 
    }

    protected HashSet<Vector2Int> CreateRectangle(int width, int height, Vector2Int spawnPosition){
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        
        Vector2Int positionToAdd;
        int diffWallSpawn = (int) (width/2f);
        for (int i = 0; i < width; i++){
            for (int j = 0; j < height; j++){
                positionToAdd = Vector2Int.RoundToInt(new Vector2(i - diffWallSpawn,spawnPoint.position.y + j - 1));
                path.Add(positionToAdd);
            }
        }
        return path;
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

    public bool IsRoomCleared()
    {
        return roomCleared;
    }

    public int GetLastRoom(){
        return lastRoom;
    }

    public Room GetCurrentRoom(){
        return currentRoom;
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions){
        PaintTiles(floorPositions,floorTilemap,floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile){
        foreach (var position in positions){
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition,tile);
        }
    }

    public void Clear(){
        floorTilemap.ClearAllTiles();
    }

    public int GetRandomHeight (int min, int max){
        return UnityEngine.Random.Range(min, max + 1);
    }

    public int GetRandomWidth(int min, int max){
        int var = UnityEngine.Random.Range(min, max + 1);
        if (var % 2 == 1){
            if (var != min){
                var -= 1;
            } else {
                var += 1;
            } 
        }
        return var;
    }
}
