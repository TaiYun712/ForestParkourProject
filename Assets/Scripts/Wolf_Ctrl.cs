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

    public enum WolfState {showUp,idle,patrolling,chasing,attack}
    public WolfState currentState;
    public Animator anim;
    public GameObject wolfCam;

    public float waitTime, waitChance;
    public float waitCounter;

    public float chaseDistance, chaseSpeed,attackDistance;

    public AudioSource wolfSound;
    public int eatCount = 0;
    public bool hasEaten = false;

    public static bool isFull = false;
    void Start()
    {
        theDwarf = FindObjectOfType< Dwarf_Ctrl>();

       foreach(Transform pp in patrolPoints)
       {
            pp.parent = null;
       } 

       currentState = WolfState.idle;

        waitCounter = waitTime;

        wolfCam.SetActive(false);
        gameObject.SetActive(false);
        lookTarget = patrolPoints[0].position;

        eatCount = 0;
        isFull = false;
    }

    void Update()
    {
        switch (currentState)
        {
            case WolfState.showUp:
                  
                wolfCam.SetActive(true);

                anim.SetBool("moveing", false);

                hasEaten = false;

                yStore = wolfRB.velocity.y;
                wolfRB.velocity = new Vector3(0f, yStore, 0f);

                Invoke("OverShowUp",5f);
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

            case WolfState.attack:
                anim.SetTrigger("attack");              
                hasEaten = true;

                Invoke("EatPlayer", 0.5f);
                break;
           
        }

        if (WolfActivator.wolfIsShowUp == true)
        {
            currentState = WolfState.showUp;
        }

        if(Vector3.Distance(theDwarf.transform.position,transform.position) < chaseDistance)
        {
            currentState = WolfState.chasing;
        }

        if (Vector3.Distance(theDwarf.transform.position, transform.position) < attackDistance && hasEaten == false)
        {
            currentState = WolfState.attack;
            
        }
        

        if (Vector3.Distance(transform.position, patrolPoints[4].position) < 1)
        {
            currentState = WolfState.idle;
            currentPatrolPoint = 0;
            waitCounter = 0;
        }

        lookTarget.y = transform.position.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime);

        if(eatCount >= 3)
        {
            isFull = true;
            gameObject.SetActive(false);
        }
        else
        {
            isFull = false;
        }
        
    }

    public void NextPatrolPoint()
    {
        if(Random.Range(0f,100f) <= waitChance)
        {
            waitCounter = waitTime;
            currentState = WolfState.idle;
        }
        else
        {
            currentPatrolPoint++;
            
        }    
    }

    public void OverShowUp()
    {
        wolfCam.SetActive(false);
        WolfActivator.wolfIsShowUp = false;
        LevelManager.instance.isPlaying = true;

        currentState = WolfState.idle;
        currentPatrolPoint = 0;
    }

    void EatPlayer()
    {
        eatCount++;
        Debug.Log("¦Y¤F"+eatCount);

        if (LevelManager.instance.currentLife > 1)
        {
            LevelManager.instance.ReSpawn();
            wolfSound.Play();
        }
        else
        {
            LevelManager.instance.NoMoreLife();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, chaseDistance);

        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, attackDistance);

    }


}
