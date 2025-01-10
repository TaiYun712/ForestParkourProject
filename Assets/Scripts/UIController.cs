using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image fadeScreen;
    public bool fadeOut, fadeIn;
    public float fadeSpeed;

    public Slider healthSlider;
    public TMP_Text healthText,timeText;

    public TMP_Text coinText,crystalText;

    public TMP_Text lifeText; //生命數
    public Image currentLifeImage; //目前玩家
    public Sprite[] lifeImage; //小矮人圖片

    public GameObject titleScreen,gamePlayScreen,failScreen,winScreen;
    public Button startBt, quitBt,backBt;
    public GameObject titleCam;

    public AudioSource titleBGM;
    public AudioSource gameBGM;
    public AudioSource buttonSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        titleScreen.SetActive(true);
        gamePlayScreen.SetActive(false);
        failScreen.SetActive(false);
        winScreen.SetActive(false);
        if (titleScreen.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            LevelManager.instance.isPlaying =false;
        }
         
        titleBGM.Play();
    }

    public void StartGame()
    {
        buttonSound.Play();
        titleBGM.Stop();
        gameBGM.Play();

        LevelManager.instance.isPlaying = true;
        titleCam.SetActive(false);

        titleScreen.SetActive(false);
        gamePlayScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;

    }

    public void CloseGame()
    {
        Debug.Log("關閉遊戲");
        buttonSound.Play();
        Application.Quit();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("GamePlay");

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
