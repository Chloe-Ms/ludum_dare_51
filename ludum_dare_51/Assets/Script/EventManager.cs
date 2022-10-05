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
    Hole,
    Dark
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
    private RoomModificationType currentModType;
    [SerializeField] private int maxNumberTilesFrozen = 5;
    [SerializeField] private int minNumberTilesFrozen = 1;
    [SerializeField] private int maxNumberTilesHole = 5;
    [SerializeField] private int minNumberTilesHole = 1;
    [SerializeField] Tilemap holeTilemap;
    [SerializeField] Tilemap freezeTilemap;
    [SerializeField] Tilemap previewTilemap;
    [SerializeField] private TileBase holeTile;
    [SerializeField] private TileBase freezeTile;
    [SerializeField] private TileBase preview;
    [SerializeField] private int nbTilesVidesParLigne;
    private RoomModificationType[,] mapMod;
    private bool enoughPlacesHole = true;
    [SerializeField] private TimerDisplay timerDisplay;
    [SerializeField] private LightManager lightManager;
    public bool timerPause = false;
    private HashSet<Vector2Int> map;
    private bool startTimer = true;
    void Start()
    {
        timer = waitTime;
    }


    void Update()
    {
        if(!timerPause){
            timer -= Time.deltaTime;
        }
        if(timer <= 4f && startTimer){
            startTimer = false;
            ChooseRoomModification();
        }
        
        if (timer < 0)
        {
            StartCoroutine(ApplyRoomModification());
            timer = waitTime - timer;
        }
        timerDisplay.SetTime(timer);
    }

    void ResetTimer()
    {
        timer = waitTime;
    }

    void ChooseRoomModification(){
        
        if(lastModType == RoomModificationType.Dark && !enoughPlacesHole) currentModType = RoomModificationType.None;
        else do {
            currentModType = (RoomModificationType)Random.Range(2, System.Enum.GetValues(typeof(RoomModificationType)).Length);
        } while (!enoughPlacesHole && currentModType == RoomModificationType.Hole);
        Debug.Log(currentModType);
        if (currentModType == RoomModificationType.Frost){
            SelectTiles(minNumberTilesFrozen, maxNumberTilesFrozen,previewTilemap,preview,RoomModificationType.Frost);
        }
        if (currentModType == RoomModificationType.Hole){
            SelectTiles(minNumberTilesHole, maxNumberTilesHole,previewTilemap,preview,RoomModificationType.Hole);
        }
    }

    IEnumerator ApplyRoomModification()
    {
        Debug.Log("Apply");
        Debug.Log(lastModType + " "+currentModType);
        if(lastModType == RoomModificationType.Dark) lightManager.LerpLight(1);
        lastModType = currentModType;
        previewTilemap.ClearAllTiles();
        switch(currentModType) 
        {
            case RoomModificationType.Frost:
                roomManager.PaintTiles(map,freezeTilemap,freezeTile);
                break;
            case RoomModificationType.Hole:
                roomManager.PaintTiles(map,holeTilemap,holeTile);
                break;
            case RoomModificationType.Dark:
                lightManager.LerpLight(0);
                break;
        }
        yield return new WaitForSeconds(6f);
        ChooseRoomModification();
    }

    void SelectTiles(int min, int max,Tilemap tilemap, TileBase tile, RoomModificationType mod){
        int numberOfTiles = Random.Range(min, max+1);
        map = new HashSet<Vector2Int>();
        for(int i = 0;i < numberOfTiles;i++){
            if (!CheckMapFull()){
                Vector2Int vec = SelectTile();
                mapMod[vec.x,vec.y] = mod;
                vec.x = vec.x - roomManager.width/2;
                vec.y = vec.y + (int)roomManager.spawnPoint.position.y - 1;
                map.Add(vec);
            }       
        }
        roomManager.PaintTiles(map,tilemap,tile);
    }

    Vector2Int SelectTile(){

        int width = roomManager.width;
        int height = roomManager.height;
        int randW,randH;
        bool res;
        int test = 0;
        do {
              randW = Random.Range(0,width);
              randH = Random.Range(0,height);  
              res = (mapMod[randW,randH] != RoomModificationType.None && mapMod[randW,randH] != RoomModificationType.Hole) || (lastModType == RoomModificationType.Hole && CheckHolesNear(randW,randH));
              if (lastModType == RoomModificationType.Hole && CheckHolesNear(randW,randH)){
                test++;
              }
        } while (res && test < 10);
        if (test >= 10){
            enoughPlacesHole = false;
        }

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

    public bool CheckMapFull(){
        int width = roomManager.width;
        int height = roomManager.height;
        bool full = true;
        int i = 0, j = 0,nbPerRow = 0;
        while(j < height && full){
            i = 0;
            while(i < width && full){
                
                if (mapMod[i,j] != RoomModificationType.None){
                    nbPerRow++;
                }
                i++;
            }
            
            if (j == 0 || j == (height - 1)){
                if (nbPerRow < roomManager.width - nbTilesVidesParLigne - 2){
                    full = false;
                }
            }else {
                if (nbPerRow < roomManager.width - nbTilesVidesParLigne){
                    full = false;
                }
            }
            
            nbPerRow = 0;
            j++;
        }
        return full;
    }

    public bool CheckHolesNear(int x, int y){
        int width = roomManager.width;
        int height = roomManager.height;
        bool res = false;

        if (x>0 && mapMod[x-1,y] == RoomModificationType.Hole){
            res = true;
        }
        if (x<width-1 && mapMod[x+1,y] == RoomModificationType.Hole){
            res = true;
        }
        if (y>0 && mapMod[x,y-1] == RoomModificationType.Hole){
            res = true;
        }
        if (y<height-1 && mapMod[x,y+1] == RoomModificationType.Hole){
            res = true;
        }
        return res;
    }

    public void Clear()
    {
        holeTilemap.ClearAllTiles();
        freezeTilemap.ClearAllTiles();
        //Door
    }
}
