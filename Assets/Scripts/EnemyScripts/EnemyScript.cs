using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    

    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerDetection playerDetection;
    [SerializeField] private InMeleeRangeDetection inMeleeRangeDetection;
    [SerializeField] private SphereCollider RightHandCol;
    [SerializeField] private SphereCollider LeftHandCol;
    private Coroutine waiting = null;
    private Coroutine attacking = null;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string animationState = "Moving";
        if (inMeleeRangeDetection.playerInRange)
        {
            animationState = "Attacking";
            agent.destination = transform.position; //stop the enemy
            waitFor(0.5f, () =>
            {
                if (!RightHandCol.enabled)
                    RightHandCol.enabled = true;
                if (!LeftHandCol.enabled)
                    LeftHandCol.enabled = true;
            });
        }
        else
        {
            if(attacking == null)
            {
                EnemyMovement();
                //Debug.Log("moving");

                if (RightHandCol.enabled)
                    RightHandCol.enabled = false;
                if (LeftHandCol.enabled)
                    LeftHandCol.enabled = false;
            }
        }
        AnimationHandler(animationState);
    }

    private void EnemyMovement()
    {
        if (playerDetection.playerInRange)
        {
            agent.destination = target.position; //Follow Player
            agent.speed = 3.5f;
        }
        else //Random movement
        {   
            var rnd = new System.Random();
            waitFor(rnd.Next(5, 20) / 10, () => {
                agent.destination = RandomNavmeshLocation(5);
                agent.speed = rnd.Next(1, 4) % 2 == 0 ? 2 : 3.5f;
                Debug.Log("Random movement");
            });
        }
    }

    private void AnimationHandler(string state)
    {
        switch (state)
        {
            case "Moving":
                movementAnimation();
                break;
            case "Attacking":
                if (attacking == null)
                    attacking = StartCoroutine(attackAnimation());
                break;
        }
    }

    private void movementAnimation()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private IEnumerator attackAnimation()
    {
        animator.SetBool("Attack2", true);
        yield return new WaitForSeconds(1.7f);
        animator.SetBool("Attack2", false);
        attacking = null;
    }

    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private IEnumerator doWait(float duration, Action actionAfterWait)
    {
        yield return new WaitForSeconds(duration);
        actionAfterWait();
        waiting = null;
    }

    private void waitFor(float duration, Action actionAfterWait)
    {
        if(waiting == null)
        {
            waiting = StartCoroutine(doWait(duration, actionAfterWait));
        }
    }
}
