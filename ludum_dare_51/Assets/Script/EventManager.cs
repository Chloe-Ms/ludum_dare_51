using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum RoomModificationType
{
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
    private float waitTime = 10.0f;
    public RoomManager roomManager;
    private RoomModificationType lastModType;
    public ModificationsPerRoom[] mods;
    [SerializeField] private int maxNumberTilesFrozen = 5;
    [SerializeField] private int minNumberTilesFrozen = 1;
    [SerializeField] private int maxNumberTilesHole = 5;
    [SerializeField] private int minNumberTilesHole = 1;

    void Start()
    {
        timer = waitTime;
        timerText.text = timer + "";
    }


    void Update()
    {
        timerText.text = timer + "";
        timer -= Time.deltaTime;

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
        //Select random change (not the same as the last one)
        RoomModificationType mod;
        do {
            mod = (RoomModificationType)Random.Range(0, System.Enum.GetValues(typeof(RoomModificationType)).Length);
        } while (mod == lastModType);
        lastModType = mod;
        
        //GameObject modGO = Instantiate(mods[roomManager.GetLastRoom()].Modifications[(int)mod]) as GameObject;
        //modGO.transform.parent = roomManager.GetCurrentRoom().transform.Find("Grid").transform;
        switch(mod){
            case RoomModificationType.Frost:
                
            break;
            case RoomModificationType.Hole:

            break;
        }
    }

    void SelectTiles(ModificationsPerRoom mod){

        int width = roomManager.width;
        int height = roomManager.height;
        
        int randW = Random.Range(0,width);
    }
}
