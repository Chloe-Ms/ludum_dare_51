using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject panel;
    private bool cLaPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !cLaPause)
        {
            cLaPause = true;
            Time.timeScale = 0f;
            panel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && cLaPause)
        {
            unPause();
        }

    }
    public void unPause()
    {
        cLaPause = false;
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}