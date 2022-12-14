using System;
using System.Runtime.ExceptionServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum TypeRoom{
    Sol,
    Cuisine,
    Labo,
    Machine,
    Boss
}

public class RoomManager : MonoBehaviour
{
    private GameObject player;
    public int nbRoomsBeforeBoss = 9;
    [SerializeField] public bool roomCleared = false;
    [SerializeField] private EventManager eventManager;

    [SerializeField] Tilemap floorTilemap;
    [SerializeField] Tilemap wallsTilemap;
    [SerializeField] private TileBase[] floorTiles;
    public TileBase[] bossWallsTiles;
    public TileBase[] solWallsTiles;
    public TileBase[] cuisineWallsTiles;
    public TileBase[] laboWallsTiles;
    public TileBase[] machineWallsTiles;
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
    [SerializeField]private GameObject teleport;
    private GameObject[] teleporter;
    [SerializeField]private List<GameObject> recompenses;
    private GameObject recompense = null;
    private TypeRoom type;
    public bool isFinished = false;
    private GameObject clone = null;

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
            if (nbRoomsBeforeBoss > 0){
                type = (TypeRoom)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(TypeRoom)).Length - 1);
            } else {
                type = TypeRoom.Boss;
            }
            
            PaintFloorTiles(map);
            PaintWallsTiles();
            if (nbRoomsBeforeBoss > 0){
                MoveTeleport();
            } else {
                for(int i = 0; i < teleporter.Length; i++){
                    Destroy(teleporter[i]);
                } 
            }
            eventManager.createMap(width,height);
            CreateEnemies();
            
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
        TileBase[] tiles = null;
        switch(type){
            case TypeRoom.Sol:
            tiles = solWallsTiles;
            break;
            case TypeRoom.Cuisine:
            tiles = cuisineWallsTiles;
            break;
            case TypeRoom.Labo:
            tiles = laboWallsTiles;
            break;
            case TypeRoom.Machine:
            tiles = machineWallsTiles;
            break;
            case TypeRoom.Boss:
            tiles = bossWallsTiles;
            break;
            
        }

        Vector2Int positionToAdd;
        int diffWallSpawn = (int) (width/2f);
        Vector3Int tilePosition;
        for (int i = 0; i < width; i++){
            positionToAdd = Vector2Int.RoundToInt(new Vector2(i - diffWallSpawn,spawnPoint.position.y -1)); //Mur du bas 
            tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
            wallsTilemap.SetTile(tilePosition,tiles[6]);
            if (nbRoomsBeforeBoss > 0){
                positionToAdd = Vector2Int.RoundToInt(new Vector2(i - diffWallSpawn,spawnPoint.position.y + height - 1)); //Mur du haut
            } else {
                positionToAdd = Vector2Int.RoundToInt(new Vector2(i - diffWallSpawn,spawnPoint.position.y + height - 2)); //Mur du haut
            }
            
            tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
            wallsTilemap.SetTile(tilePosition,tiles[7]);
        }
        positionToAdd = Vector2Int.RoundToInt(new Vector2(- diffWallSpawn,spawnPoint.position.y -1));  //Bas g
        tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
        wallsTilemap.SetTile(tilePosition,tiles[1]);
        positionToAdd = Vector2Int.RoundToInt(new Vector2(diffWallSpawn - 1,spawnPoint.position.y -1)); //Bas d
        tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
        wallsTilemap.SetTile(tilePosition,tiles[0]);

        for (int j = 1; j < height; j++){
            positionToAdd = Vector2Int.RoundToInt(new Vector2( - diffWallSpawn,spawnPoint.position.y + j - 1)); //Mur gauche
            tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
            wallsTilemap.SetTile(tilePosition,tiles[5]);
            positionToAdd = Vector2Int.RoundToInt(new Vector2(width - diffWallSpawn - 1,spawnPoint.position.y + j - 1)); //Mur droite
            tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
            wallsTilemap.SetTile(tilePosition,tiles[4]);
        }
        if (nbRoomsBeforeBoss == 0){
            positionToAdd = Vector2Int.RoundToInt(new Vector2(- diffWallSpawn,spawnPoint.position.y + height-2));  //Haut g
            tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
            wallsTilemap.SetTile(tilePosition,tiles[2]);
            positionToAdd = Vector2Int.RoundToInt(new Vector2(diffWallSpawn - 1,spawnPoint.position.y + height -2)); //Haut d
            tilePosition = wallsTilemap.WorldToCell((Vector3Int) positionToAdd);
            wallsTilemap.SetTile(tilePosition,tiles[3]);
        }
        colliderL.transform.position = new Vector2(-diffWallSpawn + 0.02f,colliderL.transform.position.y); //Gauche
        colliderR.transform.position = new Vector2((int)(width - diffWallSpawn) - 0.02f,colliderR.transform.position.y); //Droite
        colliderD.transform.position = new Vector2(colliderD.transform.position.x,spawnPoint.position.y - 1  + 0.02f); //Bas
        colliderU.transform.position = new Vector2(colliderU.transform.position.x,spawnPoint.position.y + height -1  - 0.02f); //Haut
    
    }

    public void ChangeRoom(GameObject rec)
    {
        Clear();
        if (nbRoomsBeforeBoss > 0){
            for (int i = 0; i < 4; i++){
                teleporter[i].gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsActivated",false);
            }
        }
        
        roomCleared = false;
        recompense = rec;
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
        if (nbRoomsBeforeBoss > 0){
            PaintTiles(floorPositions,floorTilemap,floorTiles[(int)type]);
        } else {
            PaintTiles(floorPositions,floorTilemap,floorTiles[System.Enum.GetValues(typeof(TypeRoom)).Length - 1]);
        }
    }

    public void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile){
        foreach (var position in positions){
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition,tile);
        }
    }

    private void AddTeleport(){
        teleporter[0] = Instantiate(teleport,new Vector2(0,0),Quaternion.identity); //Droite-bas
        teleporter[1] = Instantiate(teleport,new Vector2(0,0),Quaternion.identity);//droite-haut
        teleporter[2] = Instantiate(teleport,new Vector2(0,0),Quaternion.identity); //Gauche-bas
        teleporter[3] = Instantiate(teleport,new Vector2(0,0),Quaternion.identity);//Gauche-haut
        ChooseRecompenses();
    }

    public void MoveTeleport(){
        int diffWallSpawnW = (int) (width/2f);
        float diffWallSpawnH = height/2f;
        float center = spawnPoint.position.y + (height/2f)-3/2f;

        teleporter[0].transform.position = new Vector2(diffWallSpawnW-1,spawnPoint.position.y-1);
        teleporter[1].transform.position = new Vector2(diffWallSpawnW-1,Mathf.Floor(center + diffWallSpawnH));
        teleporter[2].transform.position = new Vector2(-diffWallSpawnW,spawnPoint.position.y-1);
        teleporter[3].transform.position = new Vector2(-diffWallSpawnW,Mathf.Floor( center + diffWallSpawnH));
        ChooseRecompenses();
    }
    

    public void Clear(){
        floorTilemap.ClearAllTiles();
        wallsTilemap.ClearAllTiles();
        eventManager.Clear();
        enemiesInGame.Clear();
        if (clone != null)
        {
            Destroy(clone);
        }
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
        if (nbRoomsBeforeBoss > 0){
            for (int i = 0; i < 4; i++){
                teleporter[i].gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsActivated",true);
            }
            if (recompense != null){
                SpawnRecompense();
            }
        } else {
            isFinished = true;
        }
        nbRoomsBeforeBoss -= 1;
        
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
         if (enemiesInGame.Count == 0){
            RoomCleared();
         }
    }

    public void SpawnRecompense(){
        int bottom = (int)(spawnPoint.position.y - 1), top = (int)(spawnPoint.position.y + height - 1);
        Vector2 vec = new Vector2(spawnPoint.position.x,bottom + top /2f);
        clone = Instantiate(recompense,vec,Quaternion.identity);
        //On renomme le gameobject pour le script player_weapons
        if (clone.name.Contains("Pistol")){
            clone.name = "Pistol";
        } else if (clone.name.Contains("AssaultRifle")){
             clone.name = "AssaultRifle";
        } else if (clone.name.Contains("fusil")){
            clone.name = "fusil";
        }
    }

    public void ChooseRecompenses(){
        
        for(int i = 0; i < teleporter.Length; i++){
            int rec = UnityEngine.Random.Range(0,recompenses.Count);
            teleporter[i].transform.Find("Object").GetComponent<Door>().recompense = recompenses[rec];
        }
    }
}
