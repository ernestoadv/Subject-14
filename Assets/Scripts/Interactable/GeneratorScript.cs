using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public KeyCode interactKey;
    public KeyCode interactPunch;
    public KeyCode interactKick;
    public bool isInRange = false;
    public bool isIntact = true;

    private int maxHits = 2;

    private int timesHit;
    public Sprite[] generatorSprites;
    public Sprite actualSprite;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D generatorCollider;

    public AudioSource backgroundSound;

    // Start is called before the first frame update
    void Start()
    {
        timesHit = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        generatorCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GeneratorInteraction();
        ReduceSoundSlightly();
        DamageGenerator();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = true;
            Debug.Log("Player is in range of generator object");
            Debug.Log("Actual hitcount: " + timesHit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = false;
            Debug.Log("Player is NOT in range with generator");
        }
    }

    private void GeneratorInteraction()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            Debug.Log("player is interacting with generator");
            //play ui element 
        }
    }

    private void DamageGenerator()
    {
        if (isInRange && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")))
        {
            timesHit++;
            if(timesHit == 1)
            {
                Debug.Log("actualSprite changed.");
                ChangeNextSprite(1);
            }
            Debug.Log("Player should damage the generator - actual hitcount: " + timesHit);
            if (timesHit >= maxHits)
            {
                Debug.Log("reached maxHit object will be destroyed");
                Debug.Log("sprite should be changed now.");
                ChangeNextSprite(2);
                generatorCollider.enabled = false;
                isIntact = false;
                //backgroundSound.enabled = false;
            }
        }
    }

    public bool IsGeneratorIntact()
    {
        return isIntact;
    }

    private void ChangeNextSprite(int spriteNumber)
    {
        actualSprite = generatorSprites[spriteNumber];
        spriteRenderer.sprite = actualSprite;
    }

    private void ReduceSoundSlightly()
    {
        float minVolume = 0f;
        bool reachedMinVol = false;
        Debug.Log("in ReduceSound function");
        if (!isIntact && !reachedMinVol)
        {
            Debug.Log("In if statement of the function");
            if (backgroundSound.volume == minVolume)
            {
                Debug.Log("second if state - reached Minvolume " + backgroundSound.volume);
                reachedMinVol = true;
                backgroundSound.enabled = false;
            }
            backgroundSound.volume -= 0.01f;
        }
    }
}
