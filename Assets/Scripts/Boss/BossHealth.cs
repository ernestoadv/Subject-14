using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 500;
    public int currentHealth;
    private Animator animator;
    public bool isInvulnerable = false;

    public void BossTakeDamage(int dmg)
    {
        print("boss takedmg function called");
        if (isInvulnerable)
            return;
        animator.SetTrigger("hurt");
        currentHealth -= dmg;

        if(currentHealth <= 250)
        {
            print("boss should enrage...");
            animator.SetBool("isEnraged", true);
        }

        if(currentHealth <= 0)
        {
            print("calling Die function");
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
