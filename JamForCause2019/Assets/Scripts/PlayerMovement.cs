using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D rb;
    /*
    private Vector2 moveVelocity;
    private bool isGrounded;
    public float jumpForce;
    public Vector2 jump;
    */
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void HandleMovement(float horizontal)
    {
        rb.velocity = new Vector2(horizontal*speed,rb.velocity.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving horizontally
        float horizontal = Input.GetAxisRaw("Horizontal");

        /*
         * if up arrow pressed single frame
         * 
         * set y velocity to some high value
         * 
         * every frame, subtract .25 * deltatime from y velocity until it hits ground
         */
        /*
        Vector2 moveInput = new Vector2(moveInputX, 0f);
        moveVelocity = moveInput.normalized * speed;
        */
        HandleMovement(horizontal);
    }
    private bool isGrounded()
    {

    }
}
