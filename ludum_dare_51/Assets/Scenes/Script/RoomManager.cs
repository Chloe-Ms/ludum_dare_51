using System;
using System.Runtime.ExceptionServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    private int lastRoom = -1;
    private GameObject player;
    [SerializeField] private bool roomCleared = true;
    [SerializeField] private EventManager eventManager;

    [SerializeField] Tilemap floorTilemap;
    [SerializeField] Tilemap teleporteurTilemap;
    [SerializeField] Tilemap wallsTilemap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase teleportTileOpen;
    [SerializeField] private TileBase teleportTileClose;
    [SerializeField] private TileBase wallsTile;
    public Transform spawnPoint;

    [SerializeField] private int maxHeight = 10;
    [SerializeField] private int minHeight = 5;
    [SerializeField] private int maxWidth = 10;
    [SerializeField] private int minWidth = 5;
    public int height;
    public int width;


    void Start()
    {
        LoadNewRoom();
        player = GameObject.FindWithTag("Player");
    }

    void LoadNewRoom()
    {
            width = GetRandomWidth(minWidth,maxWidth);
            height = GetRandomHeight(minHeight,maxHeight);
            
            HashSet<Vector2Int> map = CreateRectangle(width, height, Vector2Int.RoundToInt((Vector2)spawnPoint.position));
            PaintFloorTiles(map);
            PaintTeleportTiles(false);
            PaintWallsTiles();
            eventManager.createMap(width,height);
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

    public void PaintWallsTiles(){
        Vector2Int positionToAdd;
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        int diffWallSpawn = (int) (width/2f);
        for (int i = -1; i < width+1; i++){
            positionToAdd = Vector2Int.RoundToInt(new Vector2(i - diffWallSpawn,spawnPoint.position.y -2)); //Mur du bas 
            path.Add(positionToAdd);
            positionToAdd = Vector2Int.RoundToInt(new Vector2(i - diffWallSpawn,spawnPoint.position.y + height - 1)); //Mur du haut
            path.Add(positionToAdd);
        }

        for (int j = 0; j < height; j++){
            positionToAdd = Vector2Int.RoundToInt(new Vector2(-1 - diffWallSpawn,spawnPoint.position.y + j - 1)); //Mur gauche
            path.Add(positionToAdd);
            positionToAdd = Vector2Int.RoundToInt(new Vector2(width - diffWallSpawn,spawnPoint.position.y + j - 1)); //Mur droite
            path.Add(positionToAdd);
        }
        PaintTiles(path,wallsTilemap,wallsTile);
    }

    public void ChangeRoom()
    {
        Clear();
        LoadNewRoom();
        if (player != null)
        {
            player.transform.position = spawnPoint.position;
            player.GetComponent<Player_move>().activeMoveSpeed = player.GetComponent<Player_move>().moveSpeed;
        }
    }

    public bool IsRoomCleared()
    {
        return roomCleared;
    }

    public int GetLastRoom(){
        return lastRoom;
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions){
        PaintTiles(floorPositions,floorTilemap,floorTile);
    }

    public void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile){
        foreach (var position in positions){
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition,tile);
        }
    }

    private void PaintTeleportTiles(bool open){
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        int diffWallSpawnW = (int) (width/2f);
        float diffWallSpawnH = height/2f;
        float center = spawnPoint.position.y + (height/2f)-3/2f;
        path.Add(Vector2Int.RoundToInt(new Vector2(diffWallSpawnW-1,spawnPoint.position.y-1)));
        path.Add(Vector2Int.RoundToInt(new Vector2(diffWallSpawnW-1,Mathf.Floor(center + diffWallSpawnH))));
        path.Add(Vector2Int.RoundToInt(new Vector2(-diffWallSpawnW,spawnPoint.position.y-1)));
        path.Add(Vector2Int.RoundToInt(new Vector2(-diffWallSpawnW,Mathf.Floor( center +diffWallSpawnH))));

        teleporteurTilemap.ClearAllTiles();
        if (open){
            PaintTiles(path,teleporteurTilemap,teleportTileOpen);
        } else {
            PaintTiles(path,teleporteurTilemap,teleportTileClose);
        } 
    }
    

    public void Clear(){
        floorTilemap.ClearAllTiles();
        wallsTilemap.ClearAllTiles();
        teleporteurTilemap.ClearAllTiles();
        eventManager.Clear();
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
