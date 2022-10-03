using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AI_Brawler : MonoBehaviour
{
    [SerializeField]
    public int health;

    [SerializeField]
    public int speed;

    [SerializeField]
    public int range;

    GameObject[] enemis;
    
    public float attackRadius;
    public int degats = 2;
    

    public Vector2 attackPosition;
    private Vector2 attackPositionSave;
    private Collider2D[] target;
    


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
            
            attackPosition = (Vector2)transform.position + new Vector2(attackPositionSave.x, attackPositionSave.y);
           

            target = Physics2D.OverlapCircleAll(attackPosition, attackRadius);
            StartCoroutine(wait());

        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + attackPosition, attackRadius);
    }

    private void  hit()
    {
        foreach (Collider2D truc in target)
        {
            if (truc.tag == "Player")
            {
                truc.gameObject.GetComponent<Player_Life>().Damage(1);
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        hit();
    }

}
