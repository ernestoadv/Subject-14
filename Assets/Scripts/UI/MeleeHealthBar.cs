using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MeleeHealthBar : MonoBehaviour
{
    public EnemyMelee enemy;
    public Slider slider;
    public Image fillImage;
    private Camera camera;
    private RectTransform posUI;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        slider.maxValue = enemy.maxHealth;
        posUI = gameObject.GetComponent<RectTransform>();
    }

    public void TransformUI()
    {
        Transform posEnemy = enemy.GetComponent<Transform>();
        posEnemy.transform.position = new Vector3(posEnemy.position.x, posEnemy.position.y, posEnemy.position.z);
        posUI.position = camera.WorldToScreenPoint(new Vector3(posEnemy.position.x, posEnemy.position.y + 1.9f, posEnemy.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        TransformUI();

        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        slider.value = enemy.currentHealth;
    }
}
