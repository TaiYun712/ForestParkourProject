using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   public GameObject effect;
   public Transform effectPoint;

   public Animator anim;

    public int awardLife;
    bool isAward = false;

    public AudioSource chackSound;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         if ( LevelManager.instance.respawnPoint != transform.position)
         {
            LevelManager.instance.respawnPoint = transform.position;
            
            if (effect !=null)
            {
               Instantiate(effect, effectPoint.position, Quaternion.identity);
            }

            Checkpoint[] allCP = FindObjectsOfType<Checkpoint>();
            foreach (Checkpoint cp in allCP)
            {
               cp.anim.SetBool("active",false);
            }
            
            anim.SetBool("active",true);
         }
         
         if (!isAward)
         {
                chackSound.Play();

                LevelManager.instance.currentLife += awardLife;
                if (LevelManager.instance.currentLife >= LevelManager.instance.maxLife)
                {
                    LevelManager.instance.currentLife = LevelManager.instance.maxLife;
                }
                UIController.instance.UpdateLifeDisplay(LevelManager.instance.currentLife);
                LevelManager.instance.HatColors(LevelManager.instance.currentLife);
                isAward = true;
                
                PlayerHealthController.instance.FillHealth();
         } 
         
      }
   }
}
