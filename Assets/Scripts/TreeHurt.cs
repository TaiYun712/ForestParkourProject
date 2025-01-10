using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHurt : MonoBehaviour
{
    public float hurtChance;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RandomHurt();
        }
    }

    void RandomHurt()
    {
        if(Random.Range(0,100f) < hurtChance)
        {
            PlayerHealthController.instance.DamagePlayer();
           
        }
        else
        {
            if(LevelManager.instance.currentLife > 1)
            {
                LevelManager.instance.ReSpawn();

            }
            else
            {
                LevelManager.instance.NoMoreLife();
            }

        }
    }
}
