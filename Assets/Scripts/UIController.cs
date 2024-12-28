using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image fadeScreen;
    public bool fadeOut, fadeIn;
    public float fadeSpeed;

    public Slider healthSlider;
    public TMP_Text healthText,timeText;

    public TMP_Text coinText,crystalText;

    public TMP_Text lifeText; //¥Í©R¼Æ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    
    void Update()
    {
        if (fadeOut)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            
        }
        
        if (fadeIn)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            
        }
    }

    public void UpdateHealthDisplay(int health)
    {
        healthText.text = "Health:" + health + "/" + PlayerHealthController.instance.maxHealth;

        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = health;
    }

    public void UpdateLifeDisplay(int life)
    {
        lifeText.text = "x" + life;

    }

    public void FadeOut()
    {
        fadeOut = true;
        fadeIn = false;
    }
    
    public void FadeIn()
    {
        fadeOut = false;
        fadeIn = true;
    }
}
