using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log(collision.gameObject);
    //    if (collision.gameObject.CompareTag("Enemy") && !invulnerable)
    //    {
    //        hurt();
    //        invulnerable = true;
    //        StartCoroutine(InvulnerableTimer());
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !invulnerable)
        {
            hurt();
            invulnerable = true;
            StartCoroutine(InvulnerableTimer());
        }
    }
}
