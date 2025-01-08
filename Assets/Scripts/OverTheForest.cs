using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheForest : MonoBehaviour
{
    public GameObject safeHint;

    public Collider coll;
   
    void Start()
    {
        safeHint.SetActive(false);
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            safeHint.SetActive(true);
            Invoke("CloseTheHint",3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coll.isTrigger = false;

        }
    }

    

    void CloseTheHint()
    {
        safeHint.SetActive(false);
    }

}
