using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Kamikaze : MonoBehaviour
{


    [SerializeField]
    public int speed;

    [SerializeField]
    public int range;

    [SerializeField]
    public int range_BoomBoom;

    [SerializeField]
    public GameObject explosion;

    GameObject[] enemis;

    public Animator animController;
    private string currentAnimaton;

    public bool isrunning = false;


    private SpriteRenderer Sr;

    private Rigidbody2D rb;

   



    // Start is called before the first frame update
    void Start()
    {
        enemis = GameObject.FindGameObjectsWithTag("Player");
        Sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Enemy>().isDying)
        {
            foreach (GameObject Player in enemis)
            {
                if (Player != null)
                {
                    if (Vector3.Distance(transform.position, Player.transform.position) > range_BoomBoom)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                        if (Player.transform.position.x > transform.position.x)
                        {
                            Sr.flipX = true;

                        }
                        else if (Player.transform.position.x < transform.position.x)
                        {
                            Sr.flipX = false;
                        }
                        isrunning = true;
                    }
                    else if (Vector3.Distance(transform.position, Player.transform.position) <= range_BoomBoom)
                    {
                        StartCoroutine(wait());
                    }
                }
            }
        }
    }
    private void boomBoom()
    {
        speed = speed /3 ;
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Enemy enemy = GetComponent<Enemy>();
        enemy.Die();
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        animController.SetTrigger("Suicide");
        yield return new WaitForSeconds(0.5f);
        boomBoom();
    }

}
