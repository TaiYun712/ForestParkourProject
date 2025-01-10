using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHurt : MonoBehaviour
{
    public float hurtChance;

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
            RandomHurt();
        }
    }

    void RandomHurt()
    {
        if(Random.Range(0,100f) < hurtChance)
        {
            PlayerHealthController.instance.DamagePlayer();
            Debug.Log("¨ü¶Ë");
        }
        else
        {
            LevelManager.instance.ReSpawn();
            Debug.Log("¯{¦º");

        }
    }
}
