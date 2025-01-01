using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && LevelManager.instance.currentLife > 1)
    {
        
     LevelManager.instance.ReSpawn();
    }else if(other.CompareTag("Player"))
    {
     LevelManager.instance.NoMoreLife();
    }

  }
}
