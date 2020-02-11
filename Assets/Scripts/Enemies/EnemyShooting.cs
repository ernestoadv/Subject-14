using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletRef;
    public Transform bulletPos;

    public float speed;
    public float retreatSpeed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    //every 2 seconds
    public float startTimeBtwShots =  2f;

    public Transform player;
    private Animator myAnimator;

    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
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

    private void ShootBullet()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            myAnimator.SetBool("walk", true);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            myAnimator.SetBool("walk", false);
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -retreatSpeed * Time.deltaTime);
            myAnimator.SetBool("walk", true);
        }

        if (timeBtwShots <= 0)
        {
            Debug.Log("shooting enemy projectile");
            myAnimator.SetTrigger("shoot");
            Instantiate(bulletRef, bulletPos.position, bulletPos.rotation);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

}
