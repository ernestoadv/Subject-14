using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    private bool isFacingRight;  // For determining which way the player is currently facing.
    /*[SerializeField]*/ private float horizontalMoveSpeed;
    /*[SerializeField]*/ private float jumpMove;

    //Components for manipulation
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private BoxCollider2D myFeet;

    // Start is called before the first frame update
    void Start()
    {
        InitPlayerComponents();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveHorizontal();
        //Jump();
        //Move();
    }

    private void FixedUpdate()
    {
        MoveHorizontal();
        Jump();
        ImprovedJump();
        if (Input.GetAxis("Horizontal") < 0 && isFacingRight)
        {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    //horizontal moving of the character - method using the translate function
    private void MoveHorizontal()
    {
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
            {
                Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
                transform.position += movement * Time.deltaTime * horizontalMoveSpeed;
                myAnimator.SetBool("running", true);
            }
            else
            {
                FindObjectOfType<AudioManager>().PlaySound("Run");
                myAnimator.SetBool("running", false);
            }      
    }

    //simple jumping method 
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            print("Jump pressed");
            FindObjectOfType<AudioManager>().PlaySound("Jump");
            //Vector2 jumpVelocity = new Vector2(0f, jumpMove);
            //myRigidBody.velocity += jumpVelocity; 
            //myRigidBody.AddForce(Vector2.up * 700f);
            myRigidBody.velocity = Vector2.up * jumpMove;
        }
        if(myRigidBody.velocity.y == 0)
        {
            myAnimator.SetBool("jumping", false);
            myAnimator.SetBool("falling", false);
        }

        if(myRigidBody.velocity.y > 0)
        {
            print("jumping...");
            myAnimator.SetBool("jumping", true);
        }
        if(myRigidBody.velocity.y < 0)
        {
            print("falling...");
            myAnimator.SetBool("jumping", false);
            myAnimator.SetBool("falling", true);
        }
    }

    //better jumping?
    private void ImprovedJump()
    {
        float fallMultiplier = 4f;
        float lowJumpMultiplier = 2.5f;

        if(myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(myRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    //flips the character sprite according to the direction he is watching 
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        // ----old way----
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
    }

    //method to init all needed player components 
    private void InitPlayerComponents()
    {
        isFacingRight = true;
        horizontalMoveSpeed = 7f;
        jumpMove = 10f; 
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    //move method that uses an alternative approach of moving the character 
    private void Move()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0f);
            myRigidBody.MovePosition((Vector2)transform.position + (movement * Time.deltaTime * horizontalMoveSpeed));
            myAnimator.SetBool("running", true);
        }
        else
        {
            myAnimator.SetBool("running", false);
        }
    }
}
