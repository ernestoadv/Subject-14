using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public GameObject[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public GameObject EnemyUI;
    private Camera camera;
    private GameObject player;
    private int nextWave = 0;
    private float searchCountDown = 1f;
    private bool cameraNeedSmooth = false, 
                 smoothCompleted = false;
    private SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        waveCountDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if enemies are still alive
        if(state == SpawnState.WAITING)
        {
            //Begin new round
            if(!EnemyAlive())
            {
                if (nextWave + 1 <= waves.Length)
                    StartCoroutine(SpawnWave(waves[nextWave]));
                else
                {
                    cameraNeedSmooth = true;
                    smoothCompleted = false;
                }

            }
            else
            {
                return;
            }
        }

        //Check if spawning is needed
        if(waveCountDown <= 0)
        {
            //Spawn enemies
            if(state != SpawnState.SPAWNING)
            {
                if(nextWave+1 <= waves.Length)
                    StartCoroutine(SpawnWave(waves[nextWave]));
                else
                {
                    cameraNeedSmooth = true;
                    smoothCompleted = false;
                }
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // after a wave, if the player is in the right side of the camera, it will need to move smoothly
        if (cameraNeedSmooth && !smoothCompleted)
        {
            Vector3 desiredPosition = new Vector3(player.transform.position.x, camera.transform.position.y, camera.transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(camera.transform.position, desiredPosition, 4f * Time.deltaTime);
            camera.transform.position = smoothedPosition;

            // the camera moves smoothly until it reaches the player position (margin of 0.3)
            if (camera.transform.position.x >= player.transform.position.x - 0.3)
            {
                camera.GetComponent<CameraFollow>().enabled = true;
                cameraNeedSmooth = false;
                smoothCompleted = true;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave");
        state = SpawnState.SPAWNING;
        nextWave++;

        for(int i = 0; i < wave.count; i++)
        {
            if(spawnPoints.Length > 0)
            {
                int enemy = Random.Range(0, wave.enemies.Length);
                SpawnEnemy(wave.enemies[enemy]);
            }
            yield return new WaitForSeconds(1f/wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject enemy)
    {
        if(enemy.CompareTag("Enemy"))
        {
            //Set Enemy
            int spawn = Random.Range(0, spawnPoints.Length);
            GameObject EnemyCopy = Instantiate(enemy, spawnPoints[spawn].transform.position, spawnPoints[spawn].transform.rotation);
            EnemyCopy.GetComponent<Enemy>().player = GameObject.Find("Player").transform;
            if (EnemyCopy.GetComponent<Animal>())
            {
                EnemyCopy.GetComponent<Animal>().player = GameObject.Find("Player");
            }

            //Set UI
            GameObject UICopy = Instantiate(EnemyUI);
            EnemyCopy.GetComponent<Enemy>().UI = UICopy;
            UICopy.GetComponentInChildren<EnemyHealthBar>().enemy = EnemyCopy.GetComponent<Enemy>();
            UICopy.SetActive(true);
        }
        else if(enemy.CompareTag("EnemyMelee"))
        {
            //Set Enemy
            int spawn = Random.Range(0, spawnPoints.Length);
            GameObject EnemyCopy = Instantiate(enemy, spawnPoints[spawn].transform.position, spawnPoints[spawn].transform.rotation);
            EnemyCopy.GetComponent<EnemyMelee>().player = GameObject.Find("Player").transform;

            //Set UI
            //GameObject UICopy = Instantiate(EnemyUI);
            //EnemyCopy.GetComponent<EnemyMelee>().UI = UICopy;
            //UICopy.GetComponentInChildren<MeleeHealthBar>().enemy = EnemyCopy.GetComponent<EnemyMelee>();
            //UICopy.SetActive(true);
        }
    }

    bool EnemyAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("EnemyMelee") == null)
            {
                return false;
            }
        }
        
        return true;
    }
}
