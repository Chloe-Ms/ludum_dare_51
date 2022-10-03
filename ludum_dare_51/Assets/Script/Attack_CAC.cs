using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_CAC : MonoBehaviour
{
    public int degats = 2;
    public Vector2 attackPosition;
    private Vector2 attackPositionSave;
    public float attackRadius;
    public float reloadTime = 0.5f;
    public bool reloading;
    private Collider2D[] target;
    public SpriteRenderer skin;
    private Renderer rend;
    [SerializeField]private Color colorToTurnTo = Color.white;

    void Start()
    {
        if (!PlayerPrefs.HasKey("degatCAC"))
        {
            PlayerPrefs.SetInt("degatCAC", degats);
        }
        degats = PlayerPrefs.GetInt("degatCAC");

        attackPositionSave = attackPosition;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && !reloading)
        {
            rend.material.color = colorToTurnTo;
            degats = PlayerPrefs.GetInt("degatCAC");
            reloading = true;
            bool facing = GetComponent<Player_move>().facing;
            if (!facing)
            {
                attackPosition = (Vector2)transform.position + new Vector2(attackPositionSave.x, attackPositionSave.y);
            } else 
            {
                attackPosition = (Vector2)transform.position + new Vector2(-attackPositionSave.x, attackPositionSave.y);
            }

            target = Physics2D.OverlapCircleAll(attackPosition, attackRadius);
            foreach (Collider2D truc in target)
            {
                if (truc.tag == "Enemy")
                {
                    truc.gameObject.GetComponent<Enemy>().Die();
                }
            }
            StartCoroutine(waitShoot());
        }
    }

    IEnumerator waitShoot()
    {
        yield return new WaitForSeconds(reloadTime);
        colorToTurnTo = Color.white;
        rend.material.color = colorToTurnTo;
        colorToTurnTo = Color.red;
        reloading = false;                           
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + attackPosition, attackRadius);
    }
}
