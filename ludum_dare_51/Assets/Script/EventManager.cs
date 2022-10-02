using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum RoomModificationType
{
    ZeroG,
    Frost,
    Hole
}

public class EventManager : MonoBehaviour
{
    public Text timerText;
    private float timer;
    private float waitTime = 10.0f;
    public RoomManager roomManager;
    private RoomModificationType lastModType;

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
        //RoomModificationType mod;
        //do {
        //    mod = (RoomModificationType)Random.Range(0, Enum.GetValues(typeof(RoomModificationType)).Count);
        //} while (mod == lastModType);
        

        //lastModType = mod;
    }
}
