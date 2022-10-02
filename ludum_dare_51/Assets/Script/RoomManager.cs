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
    [SerializeField] Tilemap teleporteurTilemap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase teleportTile;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private int maxHeight = 10;
    [SerializeField] private int minHeight = 5;
    [SerializeField] private int maxWidth = 10;
    [SerializeField] private int minWidth = 5;
    public int height;
    public int width;
    private char[][] mapMod;

    void Start()
    {
        LoadNewRoom();
        player = GameObject.FindWithTag("Player");
    }

    void LoadNewRoom()
    {
        // int indexRoom = -1;
        // do {

        //     indexRoom = UnityEngine.Random.Range(0, rooms.Count);

        // } while (lastRoom == indexRoom || indexRoom < 0);
        // if (indexRoom != -1)
        // {
            width = GetRandomWidth(minWidth,maxWidth);
            height = GetRandomHeight(minHeight,maxHeight);
            HashSet<Vector2Int> map = CreateRectangle(width, height, Vector2Int.RoundToInt((Vector2)spawnPoint.position));
            PaintFloorTiles(map);
            PaintTeleportTiles();
            mapMod = new char[width][];
            //lastRoom = indexRoom;
        //} 
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
        //currentRoom.DeleteRoom();
    }

    public void ChangeRoom()
    {
        //DeleteCurrentRoom();
        Clear();
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

    private void PaintTeleportTiles(){
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        int diffWallSpawnW = (int) (width/2f);
        float diffWallSpawnH = height/2f;
        float center = spawnPoint.position.y + (height/2f)-3/2f;
        path.Add(Vector2Int.RoundToInt(new Vector2(diffWallSpawnW-1,spawnPoint.position.y-1)));
        path.Add(Vector2Int.RoundToInt(new Vector2(diffWallSpawnW-1,Mathf.Floor(center + diffWallSpawnH))));
        path.Add(Vector2Int.RoundToInt(new Vector2(-diffWallSpawnW,spawnPoint.position.y-1)));
        path.Add(Vector2Int.RoundToInt(new Vector2(-diffWallSpawnW,Mathf.Floor( center +diffWallSpawnH))));

        PaintTiles(path,teleporteurTilemap,teleportTile);
    }

    public void Clear(){
        floorTilemap.ClearAllTiles();
        teleporteurTilemap.ClearAllTiles();
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
