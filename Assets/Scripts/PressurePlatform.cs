using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatform : MonoBehaviour
{
    
    [SerializeField]
    private GameObject sensor;
    private bool player;
    private bool enemy;
    [SerializeField]
    private GameObject linkedObjective;
    [SerializeField]
    private bool needsABody;
    private void Update()
    {
        if(player || enemy)
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
        else if(other.tag == "Enemy")
        {
            enemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = false;
        }
        else if (other.tag == "Enemy")
        {
            enemy = false;
        }
    }

}
