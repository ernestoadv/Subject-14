using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 500;
    public int currentHealth;
    private Animator animator;
    private Camera camera;
    private RectTransform posUI;
    public bool isInvulnerable = false;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        posUI = GameObject.Find("BossSlider").GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void Update()
    {
        if(isAlive)
            TransformUI();
    }

    public void BossTakeDamage(int dmg)
    {
        print("boss takedmg function called");
        if (isInvulnerable)
            return;
        animator.SetTrigger("hurt");
        currentHealth -= dmg;

        if (currentHealth <= 250)
        {
            print("boss should enrage...");
            animator.SetBool("isEnraged", true);
        }

        if (currentHealth <= 0)
        {
            print("calling Die function");
            Die();
        }
    }

    public void TransformUI()
    {
        Transform posBoss = this.GetComponent<Transform>();
        posBoss.transform.position = new Vector3(posBoss.position.x, posBoss.position.y, posBoss.position.z);
        posUI.position = camera.WorldToScreenPoint(new Vector3(posBoss.position.x, posBoss.position.y + 1.9f, posBoss.position.z));
    }
    private void Die()
    {
        if (currentHealth <= 0 && isAlive)
        {
            print("boss should die");
            isAlive = false;
            Destroy(GameObject.Find("BossSlider"));
            GameObject.Find("LevelEnd").GetComponent<BoxCollider2D>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerInventory>().foundKeyCardOne = true;
            animator.SetTrigger("death");

        }
    }

    private void DestroyBoss()
    {
        Destroy(gameObject);
    }
}
