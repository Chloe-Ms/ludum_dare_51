using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Gun : MonoBehaviour
{
    
    private float health;

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

    public bool isrunning = false;

    const string GUN_RUN = "Run_pistol";

    const string GUN_DEATH = "Death_pistol";

    private SpriteRenderer Sr;

    public Animator animController;
    private string currentAnimaton;

    
    


    // Start is called before the first frame update
    void Start()
    {
        enemis = GameObject.FindGameObjectsWithTag("Player");
        Sr = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Enemy>().health;

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
                //Debug.Log(rb.velocity);
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
            if (Player != null){
                if (Vector3.Distance(transform.position, Player.transform.position) > Distance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                    isrunning = true;
                    if (Player.transform.position.x > transform.position.x)
                    {
                        Sr.flipX = true;

                    }
                    else if (Player.transform.position.x < transform.position.x)
                    {
                        Sr.flipX = false;
                    }

                } else{
                    shoot();
                    isrunning = false;
                }
            } 
        } 
        if(isrunning == true)
        {
            ChangeAnimationState(GUN_RUN);
        }
        if(health == 0)
        {
            
            ChangeAnimationState(GUN_DEATH);
            
        }
    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animController.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
