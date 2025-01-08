using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfActivator : MonoBehaviour
{
    public static bool wolfIsShowUp =false;
    public GameObject theWolf;
    Vector3 wolfShowUpPos;

    public GameObject fullHint;
    private void Start()
    {
        wolfShowUpPos = theWolf.transform.position; 
        fullHint.SetActive(false);
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
               fullHint.SetActive(true);
                Invoke("CloseTheHint",2f);
            }
        }
       
    }

    void CloseTheHint()
    {
        fullHint.SetActive(false);
    }


}
