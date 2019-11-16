using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jump;
    public float jumpForce;
    public float smallJumpForce;
    private bool jumpCancel;
    private SpriteRenderer spriteRenderer;
    public bool dead;//if player killed by enemy or obstacle
    public Vector3 lastCheckpoint;//if player dies, should respwan at last activated checkpoint
    private float gravScale;

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
        dead = false;
        jump = false;
        jumpCancel = false;
        gravScale = rb.gravityScale;
        Debug.Log("Test");
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

    void Update()
    {
        //checking for jump input
        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)//player starts pressing the buton
            jump = true;
        if (Input.GetKeyUp(KeyCode.Space) && !isGrounded)//player stops pressing the button
        {
            jumpCancel = true;
            Debug.Log("Jump Cancel is true");
        }
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving horizontally
        float horizontal = Input.GetAxisRaw("Horizontal");

        //checking if player is trying to jump (Normal jump)
        if(jump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jump = false;
            isGrounded = false;
        }
        
        //cancel the jump when button no longer pressed
        if(jumpCancel)
        {
            if (rb.velocity.y > smallJumpForce)
                rb.gravityScale *= 3f;
            jumpCancel = false;
            Debug.Log("Jump Cancel is false");
        }
        HandleMovement(horizontal);
        SwitchPlayers();
        if (dead)
        {
            transform.position = lastCheckpoint;
            dead = false;
        }
    }
    //checking collisions with different objects
    private void OnCollisionEnter2D(Collision2D col)
    {
        //checking if on ground
        if (col.gameObject.tag == "Ground") // GameObject is a type, gameObject is the property
        {
            isGrounded = true;
            rb.gravityScale = gravScale;
        }

        //checking if colliding with enemy
        if(col.gameObject.tag=="Enemy")
        {
            dead = true;
        }

        //checking if touched checkpoint
        if (col.gameObject.tag == "Checkpoint")
        {
            lastCheckpoint = col.transform.position;//new respawn point
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
