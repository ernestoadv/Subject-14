﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private CapsuleCollider2D myBody;

    private GameObject bulletRef;
    private GameObject shieldRef;

    public Transform punchPos;
    public Transform kickPos;
    public Transform bulletPos;
    public Transform shieldPos;

    private float punchRate = 2f;
    public float punchRange = 0.4f;
    private int punchDmg;
    private float nextPunch = 0f;

    private float kickRate = 2f;
    public float kickRange = 0.25f;
    private int kickDmg;
    private float nextKick = 0f;

    public LayerMask enemyLayers;
    public LayerMask bossLayers;

    public PlayerStats playerStats;

    private int eyeBeamSpellCost = 5;
    private int shieldSpellCost = 10;

    // Start is called before the first frame update
    void Start()
    {
        bulletRef = (GameObject)Resources.Load("Bullet");
        shieldRef = (GameObject)Resources.Load("Shield");
        InitPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Punch();
        Kick();
        EyeBeam();
        Shield();
    }

    /*
     * 
     */
    private void Punch()
    {
        if(Time.time >= nextPunch)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                print("punch...");
                myAnimator.SetTrigger("punch");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPos.position, punchRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        Debug.Log("We hit " + enemy.name + " with " + punchDmg);
                        enemy.GetComponent<Enemy>().TakeDamage(punchDmg);
                    }
                    else if (enemy.CompareTag("Boss"))
                    {
                        Debug.Log("We hit " + enemy.name + " with " + punchDmg);
                        enemy.GetComponent<BossHealth>().BossTakeDamage(punchDmg);
                    }        
                }
                nextPunch = Time.time + 1f / punchRate;
            }
        }  
    }

   /*
    * 
    */
    private void Kick()
    {
        if(Time.time >= nextKick)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                print("kick...");
                myAnimator.SetTrigger("kick");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickPos.position, kickRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        Debug.Log("We hit " + enemy.name + " with " + kickDmg);
                        enemy.GetComponent<Enemy>().TakeDamage(kickDmg);
                    }
                    else if (enemy.CompareTag("Boss"))
                    {
                        Debug.Log("We hit " + enemy.name + " with " + kickDmg);
                        enemy.GetComponent<BossHealth>().BossTakeDamage(kickDmg);
                    }
                }
                nextKick = Time.time + 1f / kickRate;
            }
        }  
    }
    private void EyeBeam()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (playerStats.currentMana >= eyeBeamSpellCost)
            {
                print("pewpew...");
                //myAnimator.SetTrigger("shoot");
                Instantiate(bulletRef, bulletPos.position, bulletPos.rotation);
                playerStats.ReduceMana(eyeBeamSpellCost);
            }
            else
            {
                print("not enough mana");
                return;
            }
        }
    }
    private void Shield()
    {
        if (Input.GetButtonDown("Ability1"))
        {
            if(playerStats.currentMana >= shieldSpellCost)
            {
                //GetComponent<PlayerMovement2D>().enabled = false;
                print("shielding...");
                Instantiate(shieldRef, shieldPos.position, shieldPos.rotation);
                //myAnimator.SetTrigger("kick");
                playerStats.ReduceMana(shieldSpellCost);
            }
            else
            {
                print("not enough mana");
                return;
            }
        }
        if (Input.GetButtonUp("Ability1"))
        {
            //GetComponent<PlayerMovement2D>().enabled = true;
        }

    }

    private void InitPlayer()
    {
        kickDmg = 25;
        punchDmg = 25; 
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<CapsuleCollider2D>();
    }

    //for adjusting the attackRange
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(kickPos.position, kickRange);
        Gizmos.DrawWireSphere(punchPos.position, punchRange);
    }
}
