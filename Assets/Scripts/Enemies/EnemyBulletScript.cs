using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private float speed = 150f;
    public int bulletDmg = 5;

    private Transform player;

    private Rigidbody2D rigidBody;
    private Vector2 target;

    public Transform bulletPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody2D>();
        //bulletPos = GetComponent<EnemyShooting>().GetBulletPos();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.velocity = player.position * speed * Time.deltaTime;
        //target = new Vector2(player.position.x, player.position.y);
        Invoke("DestroyBullet", .5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy bullet hit the player for " + bulletDmg + " dmg");
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(bulletDmg);
            DestroyBullet();
        }
        if (collision.CompareTag("Shield"))
        {
            Debug.Log("Enemy bullet hit player shield");
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
