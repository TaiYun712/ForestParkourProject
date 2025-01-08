using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitForRespawning;
    public bool respawning;

    private Dwarf_Ctrl player;
   
    public Vector3 respawnPoint;

    public int currentLife; //命數
    public int maxLife;

    public Material hat;

    public float levelTimer;
    public bool isPlaying; //遊戲是否正在進行，在暫停或封面

    public int currentCoins;// coinThreshold,currentCrystal;
    public AudioSource getSound;

    public AudioSource dieSound;
    public AudioSource failSound,failSound_2;
    bool playOverSound = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }

    private void Start()
    {
       player = FindObjectOfType<Dwarf_Ctrl>();
       respawnPoint = player.transform.position;

       currentLife = maxLife;//開局滿命

       UIController.instance.coinText.text = currentCoins.ToString();
       UIController.instance.UpdateLifeDisplay(currentLife); //目前命數
       HatColors(currentLife);

        isPlaying = false;
        playOverSound = false;
    }

    private void Update()
    {
        if (isPlaying)
        {
            levelTimer -= Time.deltaTime;
            int min = Mathf.FloorToInt(levelTimer / 60);
            int sec = Mathf.FloorToInt(levelTimer % 60);
            UIController.instance.timeText.text = string.Format("{0:00}:{1:00}", min, sec);
                   
        }

        if(currentLife <= 0 || levelTimer <= 0)
        {
            GameOver();
        }

       
     
    }

    public void ReSpawn()
    {
        if (!respawning)
        {
            respawning = true;
            StartCoroutine(ReSpawnCo());
        }
    }

    IEnumerator ReSpawnCo()
    {
        dieSound.Play();

        player.gameObject.SetActive(false);
        UIController.instance.FadeOut();
        yield return new WaitForSeconds(waitForRespawning);
        player.transform.position = respawnPoint;
        player.gameObject.SetActive(true);
        UIController.instance.FadeIn();
        respawning = false;
        
        PlayerHealthController.instance.FillHealth();
        currentLife--;
        UIController.instance.UpdateLifeDisplay(currentLife);
        HatColors(currentLife);
      
    }

    public void NoMoreLife()
    {
        player.gameObject.SetActive(false);
        currentLife--;
        UIController.instance.UpdateLifeDisplay(currentLife);       
    }

    public void GameOver()
    {       
        UIController.instance.failScreen.SetActive(true);
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        levelTimer -= 0;
        GameOverSound();
    }

    void GameOverSound()
    {
        if(!playOverSound)
        {
            failSound.Play();
            failSound_2.Play();
            playOverSound = true;
        }
    }

    public void HatColors(int lifedwarf)
    {
        switch (lifedwarf)
        {
            case 7:
                hat.color = Color.red;
                UIController.instance.currentLifeImage.sprite = UIController.instance.lifeImage[0];
                break;
            case 6:
                hat.color = new Color(1.0f,0.5f,0.0f);
                UIController.instance.currentLifeImage.sprite = UIController.instance.lifeImage[1];
                break;
            case 5:
                hat.color = Color.yellow;
                UIController.instance.currentLifeImage.sprite = UIController.instance.lifeImage[2];
                break;
            case 4:
                hat.color = Color.green;
                UIController.instance.currentLifeImage.sprite = UIController.instance.lifeImage[3];
                break;
            case 3:
                hat.color = Color.blue;
                UIController.instance.currentLifeImage.sprite = UIController.instance.lifeImage[4];
                break;
            case 2:
                hat.color = Color.gray;
                UIController.instance.currentLifeImage.sprite = UIController.instance.lifeImage[5];
                break;
            case 1:
                hat.color = Color.white;
                UIController.instance.currentLifeImage.sprite = UIController.instance.lifeImage[6];
                break;
        }
    }


    public void GetCoin()
    {
        getSound.Play();
        currentCoins++;
        
        UIController.instance.coinText.text = currentCoins.ToString();
        
        PlayerPrefs.SetInt("Coins",currentCoins);
    }
  
}
