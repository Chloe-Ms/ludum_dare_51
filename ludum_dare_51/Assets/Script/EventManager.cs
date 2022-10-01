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

    }
}
