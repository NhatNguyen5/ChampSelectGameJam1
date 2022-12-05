using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy" && !invulnerable)
        {
            hurt();
            invulnerable = true;
            StartCoroutine(InvulnerableTimer());
        }
    }
}
