using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerDetection playerDetection;
    [SerializeField] private PlayerDetection inMeleeRangeDetection;
    [SerializeField] private EnemyHitScript enemyHit;
    [SerializeField] private SphereCollider RightHandCol;
    [SerializeField] private SphereCollider LeftHandCol;
    [SerializeField] private ParticleSystem particleSystem;
    private Coroutine waiting = null;
    private Coroutine attacking = null;
    private string animationState = "Moving";
    private Transform target;
    private Player playerScript;
    public float countdown = 0;
    private ParticleSystem.EmissionModule PSEmission;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        PSEmission = particleSystem.emission;
        PSEmission.rateOverTime = 0;
        target = player;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHit.AteCookie)
        {
            cookieState();
            particleEmission();
        }
        else
        {
            attackState();
        }
        AnimationHandler(animationState);
    }

    private void particleEmission()
    {
        PSEmission.rateOverTime = countdown * 3;
        countdown = countdown > 0 ? countdown - Time.deltaTime : 0;
    }

    private void cookieState()
    {
        if (playerScript.mostRecentCookie.gameObject != null)
            target = playerScript.mostRecentCookie.transform;
        else
            target = transform;
        animationState = "Moving";
        EnemyMovement(playerDetection.cookieInRange);
        //Debug.Log("moving");
    }

    private void attackState()
    {
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
            target = player;
            if (attacking == null)
            {
                animationState = "Moving";
                EnemyMovement(playerDetection.playerInRange);
                //Debug.Log("moving");
                if (RightHandCol.enabled)
                    RightHandCol.enabled = false;
                if (LeftHandCol.enabled)
                    LeftHandCol.enabled = false;
            }
        }
    }

    private void EnemyMovement(bool targetInrange)
    {
        if (targetInrange)
        {
            agent.destination = target.position; //Follow Player
            agent.speed = 3.5f;
        }
        else //Random movement
        {
            if (!enemyHit.AteCookie)
            {
                var rnd = new System.Random();
                waitFor(rnd.Next(5, 20) / 10, () => {
                    agent.destination = RandomNavmeshLocation(5);
                    agent.speed = rnd.Next(1, 4) % 2 == 0 ? 2 : 3.5f;
                    Debug.Log("Random movement");
                });
            }
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
