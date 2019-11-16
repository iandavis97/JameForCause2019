using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //--PROPERTIES--
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
    public bool noSwitch;//prevents switching

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dead = false;
        jump = false;
        isPlayer1 = !isPlayer1;
        isPlayer2 = !isPlayer2;
        ActuallySwitch();
        jumpCancel = false;
        gravScale = rb.gravityScale;
        Debug.Log("Test");
        lastCheckpoint = transform.position;
    }
    void Update()
    {
        JumpInput();
        SwitchPlayers();

        //respawning
        if (dead)
        {
            transform.position = lastCheckpoint;
            rb.velocity = Vector3.zero;
            dead = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving horizontally
        float horizontal = Input.GetAxisRaw("Horizontal");

        HandleMovement(horizontal);
        HandleJump();
    }

    //--METHODS--
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
    private void JumpInput()
    {
        //checking for jump input
        if ((Input.GetKeyDown(KeyCode.Space) && isGrounded)||((Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)))//player starts pressing the buton
            jump = true;
        if ((Input.GetKeyUp(KeyCode.Space)&& !isGrounded)|| (Input.GetKeyUp(KeyCode.UpArrow) && !isGrounded))//player stops pressing the button
        {
            jumpCancel = true;
        }
    }
    private void HandleJump()
    {
        //checking if player is trying to jump (Normal jump)
        if (jump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jump = false;
            isGrounded = false;
        }

        //cancel the jump when button no longer pressed
        if (jumpCancel)
        {
            if (rb.velocity.y > smallJumpForce)
                rb.gravityScale *= 3f;
            jumpCancel = false;
        }
    }

    
    //checking collisions with different objects
    private void OnCollisionEnter2D(Collision2D col)
    {
        //checking if on ground
        if (col.gameObject.layer == 8)//8 is ground
        {
            isGrounded = true;
            rb.gravityScale = gravScale;
        }

        //checking if colliding with enemy
        if(col.gameObject.layer==10)//10 is Enemy
        {
            dead = true;
        }

        //checking if touched checkpoint
        if (col.gameObject.tag == "Checkpoint")
        {
            lastCheckpoint = col.transform.position;//new respawn point
            Destroy(col.gameObject);
        }
    }

    //checking triggers
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "NoSwitch")
            noSwitch = true;
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "NoSwitch")
            noSwitch = false;
    }

    //switching players
    private void SwitchPlayers()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&&!noSwitch)
        {
            ActuallySwitch();
        }
    }

    private void ActuallySwitch()
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
