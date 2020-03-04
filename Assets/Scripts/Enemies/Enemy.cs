using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public GameObject UI;
    public bool isFlipped = false;

    //health variable for "normal" enemies
    public int maxHealth = 100;
    public int currentHealth;

    private Animator myAnimator;

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
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

        myAnimator.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private bool GetEnemyStatus()
    {
        return isAlive;
    }

    private void Die()
    {
        if (currentHealth <= 0 && isAlive)
        {
            print("enemy should die");
            isAlive = false;
            Destroy(UI);
            myAnimator.SetTrigger("death");

        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
