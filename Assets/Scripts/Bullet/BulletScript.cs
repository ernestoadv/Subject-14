using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed = 20f;
    private int dmg = 30;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * speed;
        Invoke("DestroySelf", .5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Collider2D enemy = collision.GetComponent<Collider2D>();

        if (enemy != null)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("We hit " + enemy.name + " with " + dmg);
                enemy.GetComponent<Enemy>().TakeDamage(dmg);
                DestroySelf();
            }
            else if (enemy.CompareTag("Boss"))
            {
                Debug.Log("We hit " + enemy.name + " with " + dmg);
                enemy.GetComponent<BossHealth>().BossTakeDamage(dmg);
                DestroySelf();
            }
            else if (enemy.CompareTag("EnemyMelee"))
            {
                Debug.Log("We hit " + enemy.name + " with " + dmg);
                enemy.GetComponent<EnemyMelee>().TakeDamage(dmg);
                DestroySelf();
            }
            else if (enemy.CompareTag("Player"))
            {
                Debug.Log("We hit " + enemy.name + " with " + dmg);
                enemy.GetComponent<PlayerStats>().TakeDamage(dmg);
                DestroySelf();
            }
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

