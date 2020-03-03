using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        maxMana = 75;
        currentMana = maxMana;
        animator = GetComponent<Animator>();
        InvokeRepeating("RecoverManaOverTime", 2.0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    private void FixedUpdate()
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
        currentHealth = currentHealth > 0 ? currentHealth - dmg : 0;
        GameObject.Find("HealthText").GetComponent<Text>().text = "HP " + currentHealth + "/100";
    }

    public void ReduceMana(int mana)
    {
        print("mana should be reduced");
        currentMana = currentMana > 0 ? currentMana - mana : 0;
        GameObject.Find("ManaText").GetComponent<Text>().text = "MP " + currentMana + "/75";
    }

    private bool GetPlayerStatus()
    {
        return isAlive;
    }

    private void RecoverManaOverTime()
    {
        Debug.Log("Recovering mana overtime function");
        if(currentMana < 75)
        {
            Debug.Log("recovering mana...");
            currentMana += 2;
            GameObject.Find("ManaText").GetComponent<Text>().text = "MP " + currentMana + "/75";
        }
        else if(currentMana == maxMana)
        {
            Debug.Log("Mana full");
            return;
        }
    }

    private void Die()
    {
        if(currentHealth <= 0 && isAlive)
        {
            print("player should die");
            GetComponent<PlayerMovement2D>().enabled = false;
            isAlive = false;
            animator.SetTrigger("death");
        }
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Load same level
    }

}
