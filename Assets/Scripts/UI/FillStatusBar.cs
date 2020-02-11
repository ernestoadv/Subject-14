using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FillStatusBar : MonoBehaviour
{
    //script
    public PlayerStats playerStats;

    //health variables
    public Image fillHealthImage;
    public Slider healthSlider;

    //mana variables
    public Image fillManaImage;
    public Slider manaSlider;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = playerStats.getCurrentHealth();
        manaSlider.maxValue = playerStats.getCurrentMana();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateMana();
    }

    private void UpdateHealth()
    {
        if (healthSlider.value <= healthSlider.minValue)
        {
            fillHealthImage.enabled = false;
        }
        if (healthSlider.value > healthSlider.minValue && !fillHealthImage.enabled)
        {
            fillHealthImage.enabled = true;
        }
        healthSlider.value = playerStats.getCurrentHealth() ;
    }

    private void UpdateMana()
    {
        if (manaSlider.value <= manaSlider.minValue)
        {
            fillManaImage.enabled = false;
        }
        if (manaSlider.value > manaSlider.minValue && !fillManaImage.enabled)
        {
            fillManaImage.enabled = true;
        }
        manaSlider.value = playerStats.getCurrentMana();
    }
}
