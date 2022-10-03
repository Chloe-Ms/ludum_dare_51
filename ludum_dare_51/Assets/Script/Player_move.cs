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

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        canDash = true;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb.velocity = movement * activeMoveSpeed;

        if (Input.GetAxis("Horizontal") < 0)
        {
            facing = true;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            facing = false;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
            if (Input.GetKeyDown(KeyCode.Space))
        {

            if (canDash)
            {

                activeMoveSpeed = dashSpeed;
                canDash = false;
                StartCoroutine(Dash());
            }
        }
        IEnumerator Dash()
        {
            yield return new WaitForSeconds(dashLength);
            activeMoveSpeed = moveSpeed;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true; 
        }
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
