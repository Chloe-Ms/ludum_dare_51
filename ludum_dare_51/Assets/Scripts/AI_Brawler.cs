using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Brawler : MonoBehaviour
{
    [SerializeField]
    public int health;

    [SerializeField]
    public int speed;

    [SerializeField]
    public int range;

    GameObject[] enemis;


    // Start is called before the first frame update
    void Start()
    {
        enemis = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject Player in enemis)
        {
             transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }
}
