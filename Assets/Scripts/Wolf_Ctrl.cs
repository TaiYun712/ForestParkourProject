using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Wolf_Ctrl : MonoBehaviour
{
    public float moveSpeed,turnSpeed;
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    Vector3 moveDirection,lookTarget;
    float yStore;

    public Rigidbody wolfRB;

    Dwarf_Ctrl theDwarf;

    public enum WolfState {showUp,idle,patrolling,chasing}
    public WolfState currentState;
    public Animator anim;


    public float waitTime, waitChance;
    float waitCounter;

    public float chaseDistance, chaseSpeed;

    void Start()
    {
        theDwarf = FindObjectOfType< Dwarf_Ctrl>();

       foreach(Transform pp in patrolPoints)
       {
            pp.parent = null;
       } 

       currentState = WolfState.idle;

        waitCounter = waitTime;
    }

    void Update()
    {
        switch (currentState)
        {
            case WolfState.showUp:

                break;

            case WolfState.idle:
                anim.SetBool("moveing", false);

                yStore = wolfRB.velocity.y;
                wolfRB.velocity = new Vector3(0f,yStore,0f);

                waitCounter -=Time.deltaTime;
                if(waitCounter <= 0)
                {
                    currentState = WolfState.patrolling;

                    NextPatrolPoint();
                }
                break;

            case WolfState.patrolling:
                anim.SetBool("moveing",true);

                yStore = wolfRB.velocity.y;
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

                moveDirection.y = 0f;
                moveDirection.Normalize();

                wolfRB.velocity = moveDirection * moveSpeed;
                wolfRB.velocity = new Vector3(wolfRB.velocity.x, yStore, wolfRB.velocity.z);

                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= 0.1f)
                {
                    NextPatrolPoint();
                }
                else
                {
                    lookTarget = patrolPoints[currentPatrolPoint].position;
                }
                break;

            case WolfState.chasing:
                anim.SetBool("moveing", true);

                lookTarget = theDwarf.transform.position;

                yStore = wolfRB.velocity.y;
                moveDirection = theDwarf.transform.position - transform.position;

                moveDirection.y = 0f;
                moveDirection.Normalize();

                wolfRB.velocity = moveDirection*moveSpeed;
                wolfRB.velocity = new Vector3(wolfRB.velocity.x, yStore, wolfRB.velocity.z);

                break;

           
        }

        if(Vector3.Distance(theDwarf.transform.position,transform.position) < chaseDistance)
        {
            currentState = WolfState.chasing;
        }
       

        if (Vector3.Distance(transform.position, patrolPoints[4].position) < 1)
        {
            currentState = WolfState.idle;
        }

        lookTarget.y = transform.position.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime);

        
    }

    public void NextPatrolPoint()
    {
        if(Random.Range(0f,100f) < waitChance)
        {
            waitCounter = waitTime;
            currentState = WolfState.idle;
        }
        else
        {
            currentPatrolPoint++;
            if (currentPatrolPoint >= patrolPoints.Length)
            {    
                currentState = WolfState.idle;
            }
        }    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, chaseDistance);
    }


}
