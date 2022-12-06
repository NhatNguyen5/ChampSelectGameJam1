using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitBox"))
        {
            other.GetComponent<EnemyHitScript>().EatCookie();
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            PlayerHealth PHealth = other.GetComponent<PlayerHealth>();
            if (PHealth.currHealth < PHealth.maxHealth)
            {
                PHealth.heal();
                Destroy(gameObject);
            }
        }
    }
}
