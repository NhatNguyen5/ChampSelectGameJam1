using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHand") && !invulnerable)
        {
            StartCoroutine(InvulnerableTimer());
            hurt();
            invulnerable = true;
        }
    }

    private void flashEffect()
    {

    }
}
