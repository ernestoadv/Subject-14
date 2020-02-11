﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;

    private Animator animator;

    private bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        maxMana = 100;
        currentMana = maxMana;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getCurrentMana()
    {
        return currentMana;
    }
    public void TakeDamage(int dmg)
    {
        print("player is getting dmg");
        animator.SetTrigger("hurt");
        currentHealth -= dmg;
    }

    public void ReduceMana(int mana)
    {
        print("mana should be reduced");
        currentMana -= mana;
    }

    private bool GetPlayerStatus()
    {
        return isAlive;
    }

    private void Die()
    {
        if(currentHealth <= 0)
        {
            print("player should die");
            isAlive = false;
            //die animation
            //game over screen
        }
    }

}
