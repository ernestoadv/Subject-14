using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float enemySpeed;
    public float stoppingDistance;
    public float retreatDistance;
    public float retreatSpeed;

    public Transform playerPos;

    private Animator myAnimator;

    private float timeBtwShots;
    public float startTimeBtwShots = 2f;

    public GameObject enemyBulletRef;
    public Transform bulletPos;





    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        MovementSoldier();
    }

    //enemy movement function - moving towards, stoping and retreating
    private void MovementSoldier()
    {
        //if distance between player and enemy is greater than the stopping distance the enemy moves towards the player. 
        if (Vector2.Distance(transform.position, playerPos.position) > stoppingDistance)
        {
            Debug.Log("Enemy should move towards player...");
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, enemySpeed * Time.deltaTime);
            myAnimator.SetBool("walk", true);
            myAnimator.SetBool("idle", false);
        }
        //if distance is between stopping distance and retreatdistance - enemy does not move 
        else if (Vector2.Distance(transform.position, playerPos.position) < stoppingDistance && Vector2.Distance(transform.position, playerPos.position) > retreatDistance)
        {
            Debug.Log("Enemy should stop moving");
            transform.position = this.transform.position;
            myAnimator.SetBool("walk", false);
            myAnimator.SetBool("idle", true);
            ShootBullet();
        }
        //if distance between the two is smaller than the retreatdistance the enemies moves backwards. "retreats"
        else if (Vector2.Distance(transform.position, playerPos.position) < retreatDistance)
        {
            Debug.Log("Enemy should retreat..");
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, -retreatSpeed * Time.deltaTime);
            myAnimator.SetBool("walk", true);
            myAnimator.SetBool("idle", false);
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        if (timeBtwShots <= 0)
        {
            Debug.Log("shooting enemy projectile");
            myAnimator.SetBool("idle", false);
            myAnimator.SetTrigger("shoot");
            Instantiate(enemyBulletRef, bulletPos.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            //reducing time btw shots
            timeBtwShots -= Time.deltaTime;
        }
    }
}
