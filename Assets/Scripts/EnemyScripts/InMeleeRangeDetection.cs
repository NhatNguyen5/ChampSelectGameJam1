using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMeleeRangeDetection : MonoBehaviour
{
    public bool playerInRange = false;
    public bool AteCookie;
    private string target = "Player";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = AteCookie ? "Cookie" : "Player";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(target))
        {
            playerInRange = false;
        }
    }
}
