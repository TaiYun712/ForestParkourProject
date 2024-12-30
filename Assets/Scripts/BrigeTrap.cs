using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeTrap : MonoBehaviour
{
    public Rigidbody rb;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("觸發陷阱");
            rb.isKinematic = false;
        }
    }
}
