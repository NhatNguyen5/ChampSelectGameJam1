using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    protected int health;
    private int currHealth;
    [SerializeField]
    protected float timeBeforeNextHit;
    protected bool invulnerable;
    public TextMeshProUGUI HpText;
    public Slider HPSlider;

    private void Start()
    {
        currHealth = health;
        updateHealthBar();
    }

    private void Update()
    {
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void hurt()
    {
        currHealth--;
        updateHealthBar();
        Debug.Log(currHealth);
    }

    protected IEnumerator InvulnerableTimer()
    {
        yield return new WaitForSeconds(timeBeforeNextHit);
        invulnerable = false;
    }

    private void updateHealthBar()
    {
        HpText.text = currHealth.ToString() + "/" + health.ToString();
        HPSlider.value = (float)currHealth / (float)health;
    }

}
