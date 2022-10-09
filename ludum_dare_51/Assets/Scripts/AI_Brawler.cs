using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AI_Brawler : MonoBehaviour
{
    
    

    [SerializeField]
    public int speed;

    [SerializeField]
    public int range;

    GameObject[] enemis;
    
    public float attackRadius;
    public int degats = 2;
    public Animator animator;

    public Vector2 attackPosition;
    private Vector2 attackPositionSave;
    private Collider2D[] target;
    private bool isAttacking = false;

    public bool isrunning = false;

    const string BRAWL_RUN = "Run_Sword";

    

    const string BRAWL_ATTACK = "Attack_sword";

    private SpriteRenderer Sr;

    public Animator animController;
    private string currentAnimaton;

    // Start is called before the first frame update
    void Start()
    {
        enemis = GameObject.FindGameObjectsWithTag("Player");
        Sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject Player in enemis)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            if (transform.position.x < Player.transform.position.x){
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                ChangeAnimationState(BRAWL_RUN);
                isrunning = true;
                Sr.flipX = false;
            } else {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                ChangeAnimationState(BRAWL_RUN);

                isrunning = true;
                Sr.flipX = false;
            }
            attackPosition = (Vector2)transform.position + new Vector2(attackPositionSave.x, attackPositionSave.y);
           

            target = Physics2D.OverlapCircleAll(attackPosition, attackRadius);
            if (target.Length > 0 && !isAttacking){
                isAttacking = true;
                isrunning = false;
                StartCoroutine(wait());
            }
            

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
        isAttacking = false;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        if (animator != null)
        {
            animator.SetTrigger("IsAttacking");
        }
        yield return new WaitForSeconds(0.5f);
        hit();
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animController.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

}
