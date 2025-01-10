using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFalls : MonoBehaviour
{
    public GameObject tree;    
    public float fallingChance;
    Animator anim;

    public bool treeIsFall;
    public bool hasFall = false;

    private void Start()
    {
       anim = tree.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFall)
        {
            FallChance();

            if (treeIsFall)
            {
                Debug.Log("��ˤF");
                anim.SetTrigger("falling");
            }
            else
            {
                Debug.Log("��S��");

            }
        }  
    }

    void FallChance()
    {
        if(Random.Range(0,100f) < fallingChance)
        {
            treeIsFall = true;
        }
        else
        {
            treeIsFall = false;
        }
    }

   
}
