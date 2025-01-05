using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfActivator : MonoBehaviour
{
    public static bool wolfIsShowUp =false;
    public GameObject theWolf;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && wolfIsShowUp == false)
        {
            LevelManager.instance.isPlaying = false;
            theWolf.gameObject.SetActive(true);
            wolfIsShowUp=true;
        }
    }

  
}
