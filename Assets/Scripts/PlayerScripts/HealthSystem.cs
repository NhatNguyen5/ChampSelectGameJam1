using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float timeBeforeNextHit;
    protected bool invulnerable;

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void hurt()
    {
        health--;
        Debug.Log(health);
    }

    protected IEnumerator InvulnerableTimer()
    {
        yield return new WaitForSeconds(timeBeforeNextHit);
        invulnerable = false;
    }
}
