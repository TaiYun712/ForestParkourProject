using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

   
    public float invincLength,invincCount;

    public GameObject[] modelDisplay;
    public float flashCount, flashTime;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FillHealth();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            flashCount -= Time.deltaTime;
            if (flashCount <= 0)
            {
                flashCount = flashTime;
                foreach (GameObject piece in modelDisplay)
                {
                    piece.SetActive(!piece.activeSelf);
                }
            }
            
            if (invincCount <= 0)
            {
                flashCount = flashTime;
                foreach (GameObject piece in modelDisplay)
                {
                    piece.SetActive(true);
                }
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
            invincCount = invincLength;
            
            currentHealth--;
            if (currentHealth <= 0)
            {
                LevelManager.instance.ReSpawn();
                
            }
            
            UIController.instance.UpdateHealthDisplay(currentHealth);
        }    
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth);

    }
}
