using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeTrap : MonoBehaviour
{
    public Rigidbody[]  trapWoodrb;
    
    
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("觸發陷阱");
            for(int i = 0; i < trapWoodrb.Length; i++)
            {
                trapWoodrb[i].isKinematic = false;
            }
        }
    }
}
