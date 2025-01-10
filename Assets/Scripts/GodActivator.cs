using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodActivator : MonoBehaviour
{
    public Animator anim;

    public GameObject godCam;
    public AudioSource bolinSound;

    public GameObject godHint;

    void Start()
    {
        godCam.SetActive(false);
        godHint.SetActive(false);
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LevelManager.instance.currentCoins == 3 && LevelManager.instance.currentLife <= 1)
        {
            LevelManager.instance.isPlaying = false;
            bolinSound.Play();
            anim.SetBool("Godshow", true);
            godCam.SetActive(true);

            LevelManager.instance.currentLife = LevelManager.instance.maxLife;
            LevelManager.instance.currentCoins = 0;
            PlayerHealthController.instance.FillHealth();

            UIController.instance.coinText.text = LevelManager.instance.currentCoins.ToString();
            UIController.instance.UpdateLifeDisplay(LevelManager.instance.currentLife);
            LevelManager.instance.HatColors(LevelManager.instance.currentLife);

            Invoke("OverGod",3f);
        }
    }

    void OverGod()
    {
        anim.SetBool("Godshow", false);
        godCam.SetActive(false);
        LevelManager.instance.isPlaying = true;

        godHint.SetActive(true);
        Invoke("CloseHint", 2f);
    }

    void CloseHint()
    {
        godHint.SetActive(false);

    }


}
