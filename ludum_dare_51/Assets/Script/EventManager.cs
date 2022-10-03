using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
enum RoomModificationType
{
    None,
    Teleporter,
    Frost,
    Hole
}

[System.Serializable]
public class ModificationsPerRoom
{
    public GameObject[] Modifications;
}

public class EventManager : MonoBehaviour
{
    
    public Text timerText;
    private float timer;
    public float waitTime = 10.0f;
    public RoomManager roomManager;
    private RoomModificationType lastModType;
    [SerializeField] private int maxNumberTilesFrozen = 5;
    [SerializeField] private int minNumberTilesFrozen = 1;
    [SerializeField] private int maxNumberTilesHole = 5;
    [SerializeField] private int minNumberTilesHole = 1;
    [SerializeField] Tilemap holeTilemap;
    [SerializeField] Tilemap freezeTilemap;
    [SerializeField] private TileBase holeTile;
    [SerializeField] private TileBase freezeTile;
    private RoomModificationType[,] mapMod;

    [SerializeField] private TimerDisplay timerDisplay;

    void Start()
    {
        timer = waitTime;
        timerText.text = timer + "";
    }


    void Update()
    {
        timerText.text = timer + "";
        timer -= Time.deltaTime;
        timerDisplay.SetTime(timer);

        if (timer < 0)
        {
            ApplyRoomModification();
            timer = waitTime - timer;
        }
    }

    void ResetTimer()
    {
        timer = waitTime;
    }

    void ApplyRoomModification()
    {
        //Sélectionne le type d'événement
        RoomModificationType mod;
        do {
            mod = (RoomModificationType)Random.Range(2, System.Enum.GetValues(typeof(RoomModificationType)).Length);
        } while (mod == lastModType);
        lastModType = mod;
        
        switch(mod){
            case RoomModificationType.Frost:
                SelectTiles(minNumberTilesFrozen, maxNumberTilesFrozen,freezeTilemap,freezeTile,RoomModificationType.Frost);
            break;
            case RoomModificationType.Hole:
                SelectTiles(minNumberTilesHole, maxNumberTilesHole,holeTilemap,holeTile,RoomModificationType.Hole);
            break;
        }
    }

    void SelectTiles(int min, int max,Tilemap tilemap, TileBase tile, RoomModificationType mod){
        int numberOfTiles = Random.Range(min, max+1);
        HashSet<Vector2Int> map = new HashSet<Vector2Int>();
        for(int i = 0;i < numberOfTiles;i++){
            Vector2Int vec = SelectTile();
            mapMod[vec.x,vec.y] = mod;
            vec.x = vec.x - roomManager.width/2;
            vec.y = vec.y + (int)roomManager.spawnPoint.position.y - 1;
            map.Add(vec);
                    
        }
        roomManager.PaintTiles(map,tilemap,tile);
    }

    Vector2Int SelectTile(){

        int width = roomManager.width;
        int height = roomManager.height;
        int randW,randH;
        do {
              randW = Random.Range(0,width);
              randH = Random.Range(0,height);
        } while (mapMod[randW,randH] != RoomModificationType.None);
        return new Vector2Int(randW,randH);
    }

    public void createMap(int width,int height){
        mapMod = new RoomModificationType[width,height];
        for(int i = 0; i <width;i++){
            for(int j = 0; j < height;j++){
                mapMod[i,j] = RoomModificationType.None;
            }
        }
        mapMod[0,0] = RoomModificationType.Teleporter;
        mapMod[width-1,0] = RoomModificationType.Teleporter;
        mapMod[width-1,height-1] = RoomModificationType.Teleporter;
        mapMod[0,height-1] = RoomModificationType.Teleporter;
    }
}
