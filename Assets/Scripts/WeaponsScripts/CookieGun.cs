using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookieGun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject cookiePrefab;
    public float delayBetweenShot = 1;
    public float cookieSpeed;
    public float cookieMaxAge;
    private Coroutine shootCookieCoroutine = null;
    private Vector3 pointA = Vector3.zero;
    private Vector3 pointB = Vector3.zero;

    private void Update()
    {
        //Debug.DrawLine(pointA, pointB, Color.white, 2.5f);
    }

    public void shootCookie(Vector3 target)
    {
        if (shootCookieCoroutine == null)
            shootCookieCoroutine = StartCoroutine(doShootCookie(target, delayBetweenShot));
    }

    private IEnumerator doShootCookie(Vector3 target, float wait)
    {
        //pointA = target;
        //pointB = firePoint.position;
        Vector3 aimDir = (target - firePoint.position).normalized;
        GameObject thisCookie = Instantiate(cookiePrefab, firePoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        Rigidbody thisCookieRb = thisCookie.GetComponent<Rigidbody>();
        thisCookieRb.AddForce(firePoint.forward * cookieSpeed, ForceMode.Impulse);
        Destroy(thisCookie, cookieMaxAge);
        yield return new WaitForSeconds(wait);
        shootCookieCoroutine = null;
    }
}
