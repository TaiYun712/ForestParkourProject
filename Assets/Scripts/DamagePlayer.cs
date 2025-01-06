using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public AudioSource hurtSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
          hurtSound.Play();
          PlayerHealthController.instance.DamagePlayer();
        }
    }
}
