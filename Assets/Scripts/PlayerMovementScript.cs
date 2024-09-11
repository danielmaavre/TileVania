using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    [SerializeField] GameObject bulllet;
    [SerializeField] Transform Gun;
    
    float gravityScaleAtStart;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;
    Vector2 playerVelocity;
    GameSession gameSession;
    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        
    }

    void Update()
    {
        if(!isAlive){
            return;
            
        }else{
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }

    }

    void OnJump(InputValue value){
        bool isTouchingGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));        

        if(!isTouchingGround){
            return;
        }else{
            if(value.isPressed){
                myRigidbody.velocity += new Vector2(0f,jumpSpeed);
            }
        }
        
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    void OnFire(InputValue value){
        if(!isAlive){return;}
        Instantiate(bulllet,Gun.position,transform.rotation);
        myAnimator.SetTrigger("isFiring");
    }

    void Run(){
        playerVelocity = new Vector2(runSpeed * moveInput.x, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHasHorizontalSpeed);
        //myAnimator.SetBool("isFiring",false);
    }

    void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x),1f);
        }
        
    }

    void ClimbLadder(){
        bool isTouchingLadder = feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if(!isTouchingLadder){
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing",false);
        }else{
            playerVelocity = new Vector2(runSpeed*moveInput.x,climbSpeed*moveInput.y);
            myRigidbody.velocity = playerVelocity;
            myRigidbody.gravityScale = 0;
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y)> Mathf.Epsilon;
            myAnimator.SetBool("isClimbing",playerHasVerticalSpeed);                                                      
        }
    }

    void Die(){        
        if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            gameSession = FindObjectOfType<GameSession>();
            gameSession.ProcessPlayerDeath();
        }        
    }
}
