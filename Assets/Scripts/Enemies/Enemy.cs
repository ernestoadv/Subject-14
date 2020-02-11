using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    //health variable for "normal" enemies
    private int maxHealth = 100;
    public int currentHealth;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
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

    public void TakeDamage(int dmg)
    {
        Debug.Log("calling takedmg function of enemy");
        currentHealth -= dmg;

        //play hurt animation

        myAnimator.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            myAnimator.SetBool("death", true);
            Invoke("Die", .5f);
            //Die();
        }
    }

    public void Die()
    {
        Debug.Log("enemy died");
        //die animation
        Destroy(gameObject);
        //destroy enemy object 
    }
}
