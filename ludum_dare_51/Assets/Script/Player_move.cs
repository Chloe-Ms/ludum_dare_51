using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer monSprite;

    Vector2 movement;

    public float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    public bool canDash;

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
            monSprite.flipX = true;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            monSprite.flipX = false;
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

    void ChangeSpeed()
    {
}
