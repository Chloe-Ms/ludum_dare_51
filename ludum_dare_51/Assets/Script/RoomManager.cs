using System;
using System.Runtime.ExceptionServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    private GameObject player;
    public int nbRoomsBeforeBoss = 9;
    [SerializeField] public bool roomCleared = false;
    [SerializeField] private EventManager eventManager;

    [SerializeField] Tilemap floorTilemap;
    [SerializeField] Tilemap wallsTilemap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase wallsTile;
    public Transform spawnPoint;

    [SerializeField] private int maxHeight = 10;
    [SerializeField] private int minHeight = 5;
    [SerializeField] private int maxWidth = 10;
    [SerializeField] private int minWidth = 5;
    [HideInInspector] public int height;
    [HideInInspector] public int width;

    [SerializeField] private int nbEnemyMax = 8;
    [SerializeField] private int nbEnemyMin = 2;
    public int nbEnemiesLeft;
    public GameObject colliderR;
    public GameObject colliderL;
    public GameObject colliderU;
    public GameObject colliderD;
    [SerializeField] private GameObject[] enemies;
    private List<GameObject> enemiesInGame;
    [SerializeField]
    private GameObject Teleporter;
    private GameObject[] teleporter;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemiesInGame = new List<GameObject>();
        teleporter = new GameObject[4];
        AddTeleport();
        LoadNewRoom();
        
    }

    void LoadNewRoom()
    {
            width = GetRandomWidth(minWidth,maxWidth);
            height = GetRandomHeight(minHeight,maxHeight);
            
            HashSet<Vector2Int> map = CreateRectangle(width, height, Vector2Int.RoundToInt((Vector2)spawnPoint.position));
            PaintFloorTiles(map);
            PaintWallsTiles();
            MoveTeleport();
            eventManager.createMap(width,height);
            CreateEnemies();
            nbRoomsBeforeBoss -= 1;
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
        colliderL.transform.position = new Vector2(-diffWallSpawn,colliderL.transform.position.y); //Gauche
        colliderR.transform.position = new Vector2((int)(width - diffWallSpawn),colliderR.transform.position.y); //Droite
        colliderD.transform.position = new Vector2(colliderD.transform.position.x,spawnPoint.position.y - 1); //Bas
        colliderU.transform.position = new Vector2(colliderU.transform.position.x,spawnPoint.position.y + height -1); //Haut
    
    }

    public void ChangeRoom()
    {
        Clear();
        roomCleared = false;
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

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions){
        PaintTiles(floorPositions,floorTilemap,floorTile);
    }

    public void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile){
        foreach (var position in positions){
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition,tile);
        }
    }

    private void AddTeleport(){
        teleporter[0] = Instantiate(Teleporter,new Vector2(0,0),Quaternion.identity); //Droite-bas
        teleporter[1] = Instantiate(Teleporter,new Vector2(0,0),Quaternion.identity);//droite-haut
        teleporter[2] = Instantiate(Teleporter,new Vector2(0,0),Quaternion.identity); //Gauche-bas
        teleporter[3] = Instantiate(Teleporter,new Vector2(0,0),Quaternion.identity);//Gauche-haut
        
    }

    public void MoveTeleport(){
        int diffWallSpawnW = (int) (width/2f);
        float diffWallSpawnH = height/2f;
        float center = spawnPoint.position.y + (height/2f)-3/2f;

        teleporter[0].transform.position = new Vector2(diffWallSpawnW-1,spawnPoint.position.y-1);
        teleporter[1].transform.position = new Vector2(diffWallSpawnW-1,Mathf.Floor(center + diffWallSpawnH));
        teleporter[2].transform.position = new Vector2(-diffWallSpawnW,spawnPoint.position.y-1);
        teleporter[3].transform.position = new Vector2(-diffWallSpawnW,Mathf.Floor( center +diffWallSpawnH));
    }
    

    public void Clear(){
        floorTilemap.ClearAllTiles();
        wallsTilemap.ClearAllTiles();
        eventManager.Clear();
        enemiesInGame.Clear();
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
    }

    public void CreateEnemies(){
        if (nbRoomsBeforeBoss > 0){
            nbEnemiesLeft = UnityEngine.Random.Range(nbEnemyMin,nbEnemyMax+1);
            for (int i = 0; i < nbEnemiesLeft; i++){
                int index = UnityEngine.Random.Range(1,enemies.Length);
                Vector3 vec = GetRandomPosition();
                enemiesInGame.Add(Instantiate(enemies[index],vec,Quaternion.identity));
            }
        } else {
            Vector3 vec = GetRandomPosition();
            vec.x = 0f;
            enemiesInGame.Add(Instantiate(enemies[0],vec,Quaternion.identity));//Boss

            int index = UnityEngine.Random.Range(1,enemies.Length); //Le gars qui pilote
            vec.x = 0f;
            vec.y = spawnPoint.position.y + height - 2;
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
        } while (Mathf.Abs(playerPos.x - x) < 1.5f && Mathf.Abs(playerPos.y - y) < 1.5f );
        
        return new Vector3(x,y,0f);
    }

    public void RemoveEnemy(GameObject obj){
         enemiesInGame.Remove(obj);
        Debug.Log(enemiesInGame.Count);
         if (enemiesInGame.Count == 0){
            RoomCleared();
         }
         Destroy(obj);
    }
}
