using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;

    public float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    public bool canDash;

    public bool facing;

    public Player_Life life;

    public Animator animController;
    private string currentAnimaton;

    private SpriteRenderer Sr;


    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        canDash = true;
        Sr = GetComponentInChildren<SpriteRenderer>();


    }

    void Update()
    {
        if (!life.inAnimationDead)
        {

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

        
            animController.SetFloat("Speed",Mathf.Max(Mathf.Abs(movement.x) + Mathf.Abs(movement.y)));
            movement.Normalize();
            rb.velocity = movement * activeMoveSpeed;

            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f); 
                facing = true;
            }

            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                facing = false;

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (canDash)
                {
                    activeMoveSpeed = dashSpeed;
                    canDash = false;
                    //animController.SetTrigger("IsDashing");
                    StartCoroutine(Dash());
                }
            }
        }
    }

    IEnumerator Dash()
        {
            Player_Weapons dashRemaining = GetComponent<Player_Weapons>();
            if (dashRemaining.CurrentCharge > 0)
            {
                canDash = true;
            }
            else
            {
                canDash = false;
            }
            
            dashRemaining.CurrentCharge -= 1;
            yield return new WaitForSeconds(dashLength);
            activeMoveSpeed = moveSpeed;
            yield return new WaitForSeconds(dashCooldown);
        }

    public void SetSpeed(float newSpeed)
    {
        if (canDash){ //Si il est pas en train de dasher
            if (activeMoveSpeed == moveSpeed && newSpeed > 0){
                activeMoveSpeed = moveSpeed + newSpeed;
            } else if (activeMoveSpeed == moveSpeed - newSpeed && newSpeed < 0){
                activeMoveSpeed = moveSpeed;
            }
        }
    }
}
