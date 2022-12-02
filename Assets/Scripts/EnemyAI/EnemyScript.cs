using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float detectRange;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerDetection playerDetection;
    private Coroutine waiting = null;
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
        if (playerDetection.playerInRange)
        {
            agent.destination = target.position; //Follow Player
            agent.speed = 3.5f;
        }
        else
        {   //Random movement
            var rnd = new System.Random();
            waitFor(rnd.Next(5, 20)/10, ()=>{
                agent.destination = RandomNavmeshLocation(5);
                agent.speed = rnd.Next(1, 4) % 2 == 0 ? 2 : 3.5f;
                Debug.Log("Random movement");
            });
        }
        Debug.Log(agent.pathStatus);
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
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
