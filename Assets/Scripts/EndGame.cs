using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("iswin");
            UIController.instance.gameBGM.Stop();

            UIController.instance.winScreen.SetActive(true);
            LevelManager.instance.isPlaying = false;
            Cursor.lockState = CursorLockMode.None;
            LevelManager.instance.levelTimer -= 0;
        }
    }
}
