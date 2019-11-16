using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D rb;
    private bool isGrounded;
    public float jumpForce;
    private SpriteRenderer spriteRenderer;

    //setting up player switch
    public Sprite sprite1;
    public Sprite sprite2;
    public bool isPlayer1;
    public bool isPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isPlayer1 = true;
        isPlayer2 = false;
    }

    private void HandleMovement(float horizontal)
    {
        //flipping sprite
        if (GetComponent<SpriteRenderer>().flipX == false && horizontal < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(GetComponent<SpriteRenderer>().flipX == true && horizontal > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        //moving horizontally
        rb.velocity = new Vector2(horizontal*speed,rb.velocity.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving horizontally
        float horizontal = Input.GetAxisRaw("Horizontal");

        //checking if on ground to jump
        if(isGrounded==true&&Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
        }
        HandleMovement(horizontal);
        SwitchPlayers();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") // GameObject is a type, gameObject is the property
        {
            isGrounded = true;
        }
    }

    //switching players
    private void SwitchPlayers()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (spriteRenderer.sprite == sprite1) // if the spriteRenderer sprite = sprite1 then change to sprite2
            {
                spriteRenderer.sprite = sprite2;
                isPlayer1 = false;
                isPlayer2 = true;
            }
            else
            {
                spriteRenderer.sprite = sprite1; // otherwise change it back to sprite1
                isPlayer2 = false;
                isPlayer1 = true;
            }
        }


    }
}
