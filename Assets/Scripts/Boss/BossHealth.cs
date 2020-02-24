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
    private Vector2 posBoss;
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
        Destroy(GameObject.Find("BossSlider"));
        GameObject.Find("LevelEnd").GetComponent<BoxCollider2D>().enabled = true;
    }

    public void TransformUI()
    {
        Transform posBoss = this.GetComponent<Transform>();
        posBoss.transform.position = new Vector3(posBoss.position.x, posBoss.position.y, posBoss.position.z);
        posUI.position = camera.WorldToScreenPoint(new Vector3(posBoss.position.x, posBoss.position.y + 1.9f, posBoss.position.z));
    }

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
        TransformUI();
    }
}
