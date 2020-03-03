using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    #region public variables
    public Transform player;
    public LayerMask playerMask;
    public Transform punchPos;
    public int punchDmg;
    public float attackDistance; //minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public int maxHealth = 100;
    public int currentHealth;
    public bool isFlipped = false;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //store distance between player and enemy
    private bool attackMode;
    //private bool inRange; //check if player is in range
    private bool cooling; //check if enemy is cooling after attack
    private float initialTimer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        initialTimer = timer;
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("HumanPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        Debug.Log("calling enemylogic");
        EnemyLogic();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }


    void EnemyLogic()
    {
        Debug.Log("in enemylogic function");
        distance = Vector2.Distance(transform.position, target.transform.position);
        if(distance > attackDistance)
        {
            anim.SetBool("canWalk", false);
            Move();
            StopAttack();
        }
        else if(attackDistance > distance && cooling == false)
        {
            Attack();

        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }

    private void Move()
    {
        Debug.Log("in Move function");
        anim.SetBool("canWalk", true);
        Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        Debug.Log("in attack function");
        timer = initialTimer; //reset timer when player enter attack range
        attackMode = true; //to check if enemy can still attack or not 
        anim.SetBool("canWalk", false);
        anim.SetBool("attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = initialTimer;
        }
    }
    private void StopAttack()
    {
        Debug.Log("in stop attack function");
        cooling = false;
        attackMode = false;
        anim.SetBool("attack", false);
    }

    public void DamagePlayer()
    {
            Debug.Log("DamagePlayerEvent");
            Collider2D colInfo = Physics2D.OverlapCircle(punchPos.position, attackDistance, playerMask);
            if (colInfo != null && colInfo.CompareTag("HumanPlayer"))
            {
                print("player should get Dmg");
                colInfo.GetComponent<PlayerStats>().TakeDamage(punchDmg);
            }
    }

    public void TakeDamage(int dmg)
    {
        //
        Debug.Log("enemyMelee should get dmg");
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            //DIE
            Destroy(gameObject);
        }
    }

    public void TriggerCooling()
    {
        Debug.Log("cooling was triggered");
        cooling = true;
    }
}
