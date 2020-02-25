using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private float bulletSpeed = 12f;
    public int bulletDmg = 5;

    public Transform playerPos;
    private Vector2 target;

    public Transform bulletPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("HumanPlayer").transform;
        target = new Vector2(playerPos.position.x, playerPos.position.y);
        Invoke("DestroyBullet", 3.0f);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HumanPlayer"))
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
        if (collision.CompareTag("Interactable"))
        {
            Debug.Log("Enemy bullet hit interactable object");
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
