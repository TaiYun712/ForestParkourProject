using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestMod : MonoBehaviour
{
    public Transform[] checkPoints;
  
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
         

            player.SetActive(false);
            
            player.transform.position = checkPoints[0].transform.position;

            player.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            

            player.SetActive(false);

            player.transform.position = checkPoints[1].transform.position;

            player.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           

            player.SetActive(false);

            player.transform.position = checkPoints[2].transform.position;

            player.SetActive(true);
        }
    }
}
