using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Attack : MonoBehaviour
{

    private int degats = 50;
    //public Vector2 attackPosition;
    //private Vector2 attackPositionSave;
    //public float attackRadius;
    public float reloadTime = 0.5f;
    [SerializeField] private bool reloading = false;
    private Animator anim;
    private Collider2D[] target;
    private SpriteRenderer skin;

    public Transform attackPoint;
    public float attackRadius;
    //public Transform attackPoint; // Gizmo d'endroit d'attaque
    public float attackRange = 0.5f; // La taille du Gizmo
    //public LayerMask enemyLayers; // Savoir reconnaitre l'ennemi

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        skin = GetComponent<SpriteRenderer>();
        //attackPositionSave = attackPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !reloading)
        {
            reloading = true;
            anim.SetTrigger("Slashing");
            attacking();
        }
    }

    void attacking()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D truc in hitEnemies)
        {
            if (truc.tag == "Enemy")
            {
                //truc.gameObject.GetComponent<Enemy>().Die();
                truc.gameObject.GetComponent<Enemy>().TakeDamage(degats);
            }
        }
        StartCoroutine(waitShoot());
    } 

    IEnumerator waitShoot()
    {
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }

    void OnDrawGizmosSelected() // AttackPoint Gizmpo
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
