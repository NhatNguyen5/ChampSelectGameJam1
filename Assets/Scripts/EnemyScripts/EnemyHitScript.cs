using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitScript : MonoBehaviour
{
    public bool AteCookie = false;
    public float cookieEffectDuration;
    public EnemyScript enemyScript;
    private Coroutine ateCookieRoutine = null;
    public AudioClip EatSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    public void EatCookie()
    {
        if (ateCookieRoutine == null)
        {
            ateCookieRoutine = StartCoroutine(doEatCookie(cookieEffectDuration));
        }
        else
        {
            StopCoroutine(ateCookieRoutine);
            ateCookieRoutine = null;
        }
    } 

    private IEnumerator doEatCookie(float duration)
    {
        AteCookie = true;
        enemyScript.countdown = duration;
        AudioSource.PlayClipAtPoint(EatSound, transform.position, 10);
        yield return new WaitForSeconds(duration);
        AteCookie = false;
        ateCookieRoutine = null;
    }
}
