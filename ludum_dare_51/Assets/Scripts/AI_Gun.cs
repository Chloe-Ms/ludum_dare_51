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

    private int Spread;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    public int bulletSpeed;

    [SerializeField]
    public int shootDelay;

    public float shoot_time;


    // Start is called before the first frame update
    void Start()
    {
        enemis = GameObject.FindGameObjectsWithTag("Player");

    }
    void shoot()
    {
        shoot_time += Time.deltaTime;
        if (shoot_time > shootDelay)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            foreach (GameObject Player in enemis)
            {
                Vector2 direction = (Player.transform.position - bullet.transform.position).normalized;
                Vector2 force = direction * bulletSpeed;
                rb.velocity = force;
                Debug.Log(rb.velocity);
                shoot_time = 0;

                //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, Player.transform.position.y + Spread);
            }
        }
        //Spread = Random.Range(1, -2);
        
    }
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject Player in enemis)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) > Distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

            } else
            {
                shoot();
            }
        }
    }
}
