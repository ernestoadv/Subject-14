using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurGuyMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public LayerMask whatIsEnemy;

    private float horizontalMove;
    private float jumpMove;

    private bool fistAttack;
    private bool kickAttack;
    private bool shootAttack;


    //PM
    public GameObject bulletRef;
    public float shootRate;
    public Transform shootPos;
    public float shootDmg;

    float runSpeed;
    private void Start()
    {
        bulletRef = (GameObject)Resources.Load("Bullet");
    }
    void Awake()
    {
        horizontalMove = 0f;
        jumpMove = 0f;
        fistAttack = false;
        kickAttack = false;
        shootAttack = false;
        runSpeed = 40.0f;
    }

    //And make our player move here
    private void FixedUpdate()
    {
        bool jump = jumpMove > 0 ? true : false;

        controller.Move(horizontalMove, jump);
        if (fistAttack || kickAttack)
        {
            controller.Attack(fistAttack, kickAttack, shootAttack);
        }

 

    }

    //Update is called once per frame
    //We wanna get the Inputs in here
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed * Time.fixedDeltaTime;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        Fire();
        jumpMove = Input.GetAxis("Jump");
        fistAttack = Input.GetButtonDown("Punch");
        kickAttack = Input.GetButtonDown("Kick");
        
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("shoot");
            Instantiate(bulletRef, shootPos.position, shootPos.rotation);
            //bullet.transform.position = shootPos.position;
            print("boom");

            Collider2D[] damageableEnemies = Physics2D.OverlapCircleAll(shootPos.position, 5 , whatIsEnemy);
            if (damageableEnemies.Length > 0)
            {
                damageableEnemies[0].GetComponent<EnemyBehavior2D>().Damage(shootDmg);
            }
        }
    }
	
	public void OnLanding(){
        jumpMove = 0;
	}
}
