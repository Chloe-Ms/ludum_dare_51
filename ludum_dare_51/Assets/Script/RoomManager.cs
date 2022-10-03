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
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase teleportTileOpen;
    [SerializeField] private TileBase teleportTileClose;
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

    public void ChangeRoom()
    {
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

    public void RoomCleared(){
        roomCleared = true;
        PaintTeleportTiles(true);
    }

    public void CreateEnemies(){
        nbEnemiesLeft = UnityEngine.Random.Range(nbEnemyMin,nbEnemyMax+1);
        for (int i = 0; i < nbEnemiesLeft; i++){
            int index = UnityEngine.Random.Range(0,enemies.Length);
            Vector3 vec = GetRandomPosition();
            enemiesInGame.Add(Instantiate(enemies[index],vec,Quaternion.identity));
        }
    } 

    public Vector3 GetRandomPosition(){
        int diffWallSpawn = (int) (width/2f);
        int left = - diffWallSpawn, right = width - diffWallSpawn;
        int bottom = (int)(spawnPoint.position.y - 1), top = (int)(spawnPoint.position.y + height - 1);
        Vector2 playerPos = player.transform.position;
        float x,y; 
        do {
            x = UnityEngine.Random.Range( (float)left, (float) right);
            y = UnityEngine.Random.Range( (float)bottom, (float) top);
        } while (Mathf.Abs(playerPos.x - x) < 1f && Mathf.Abs(playerPos.y - y) < 1f );
        
        return new Vector3(x,y,0f);
    }

    public void RemoveEnemy(GameObject obj){
         enemiesInGame.Remove(obj);
         if (enemiesInGame.Count == 0){
            RoomCleared();
         }
    }
}
