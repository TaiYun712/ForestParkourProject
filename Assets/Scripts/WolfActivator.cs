using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfActivator : MonoBehaviour
{
    public static bool wolfIsShowUp =false;
    public GameObject theWolf;
    Vector3 wolfShowUpPos;

    private void Start()
    {
        wolfShowUpPos = theWolf.transform.position; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Wolf_Ctrl.isFull ==false)
        {
            if (other.CompareTag("Player") && wolfIsShowUp == false)
            {
                LevelManager.instance.isPlaying = false;
                theWolf.transform.position = wolfShowUpPos;
                theWolf.gameObject.SetActive(true);
                wolfIsShowUp = true;
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("¯T¹¡¤F");
            }
        }
       
    }

  
}
