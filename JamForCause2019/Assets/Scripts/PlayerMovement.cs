using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D rb;
    private bool isGrounded;
    public float jumpForce;
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
        */
        if(isGrounded==true&&Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
        }
        HandleMovement(horizontal);
    }
    /*
    private bool IsGrounded()
    {
        if (rb.velocity.y <= 0)
        {
            return true;
        }
        else
            return false;
    }*/
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") // GameObject is a type, gameObject is the property
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }
}
