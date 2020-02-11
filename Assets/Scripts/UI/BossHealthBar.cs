using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public BossHealth bossHealth;
    public Slider slider;
    public Image fillImage;

    void Start()
    {
        slider.maxValue = bossHealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        slider.value = bossHealth.currentHealth;
    }
}
