using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           // Debug.Log("刺到");
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
