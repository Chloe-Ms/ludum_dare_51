using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Gun : MonoBehaviour
{
    [SerializeField]
    public int health;

    [SerializeField]
    public int speed;

    [SerializeField]
    public int range;

    [SerializeField]
    public float Distance;

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
            if (Vector3.Distance(transform.position, Player.transform.position) > Distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

            }
        }
    }
}
