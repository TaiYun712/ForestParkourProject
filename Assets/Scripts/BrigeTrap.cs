using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeTrap : MonoBehaviour
{
    public Rigidbody[]  trapWoodrb;

    public AudioSource brokeSound;

    public bool soundPlayed =false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && soundPlayed == false)
        {
            
            for(int i = 0; i < trapWoodrb.Length; i++)
            {
                trapWoodrb[i].isKinematic = false;
            }

            brokeSound.Play();
            soundPlayed = true;
        }
    }
}
