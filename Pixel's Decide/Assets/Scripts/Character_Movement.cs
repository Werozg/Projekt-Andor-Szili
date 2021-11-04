using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character_Movement : MonoBehaviour
{
    PhotonView view;

    Collider2D collider2d;
    Rigidbody2D rigidbody2d;
    private float playerScale;
    private float distanceToGround;
    private float XCord ; 
    bool IsGrounded;
    bool canJump;
    float jumpPressedTimer = 0.2f;
    float jumpPressedTimerRemebered = 0;

    float groundedTimer = 0.2f;
    float groundedTimerRemebered = 0;


    public Transform feetPos;
    public LayerMask Ground;
    
    [Range(1,100)]
    public int speed = 1;
    public float jumpSpeed;

    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerScale = transform.localScale.x;
        view = GetComponent<PhotonView>();
    }

    void Update()
    {

        if(view.IsMine)
        {
            jumpPressedTimerRemebered -= Time.deltaTime;
            groundedTimerRemebered = -Time.deltaTime;

            //By hitting A or D in keyboard we can move our player.
            //Checking if Input is greater than 0 or not and flipping the player model by that value.
            XCord = Input.GetAxisRaw("Horizontal") * speed;

            if(IsGrounded = Physics2D.OverlapCircle(feetPos.position, 0.1f, Ground)){
                groundedTimerRemebered = groundedTimer;
            }
    
            if ( Input.GetKeyDown(KeyCode.W)){
                jumpPressedTimerRemebered = jumpPressedTimer;
            }
        }
   }
   void FixedUpdate()
   {
        if(view.IsMine)
        {
            Vector2 move = new Vector2(1,0) * XCord;
            move = new Vector2(move.x,rigidbody2d.velocity.y);
            rigidbody2d.velocity = move;

            Vector3 charecterScale = transform.localScale;
            if (XCord < 0)
            {
                charecterScale.x = -playerScale;
            } 
            if (XCord > 0)
            {
                charecterScale.x = playerScale;
            }
            transform.localScale = charecterScale;

        
            if (groundedTimerRemebered > 0 && jumpPressedTimerRemebered > 0){
                jumpPressedTimerRemebered = 0;
                groundedTimerRemebered = 0;
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x,jumpSpeed);
            }
        }
   }
}
