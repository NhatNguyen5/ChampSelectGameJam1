using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatform : MonoBehaviour
{
    
    [SerializeField]
    private GameObject sensor;
    private bool player;
    [SerializeField]
    private GameObject linkedObjective;
    [SerializeField]
    private bool needsABody;
    private int ghoulsInRange;

    private void Start()
    {
        ghoulsInRange = 0;
    }
    private void Update()
    {
        if(ghoulsInRange>0)
        {
            sensor.SetActive(true);
            linkedObjective.SetActive(false);
        }
        else
        {
            if(needsABody)
            {
                sensor.SetActive(false);
                linkedObjective.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = true;
        }
        else if(other.tag == "EnemyHitBox")
        {
            ghoulsInRange++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = false;
        }
        else if (other.tag == "EnemyHitBox")
        {
            ghoulsInRange--;
        }
    }

}
