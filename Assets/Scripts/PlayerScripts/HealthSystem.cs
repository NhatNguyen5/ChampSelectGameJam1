using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    [HideInInspector]
    public int currHealth;
    [SerializeField]
    protected float timeBeforeNextHit;
    protected bool invulnerable;
    public TextMeshProUGUI HpText;
    public Slider HPSlider;
    public AudioClip EatSound;

    private void Start()
    {
        currHealth = maxHealth;
        updateHealthBar();
    }

    private void Update()
    {
        if(currHealth <= 0)
        {
            GameManager.UpdateGameState(GameState.Lose);
            Destroy(gameObject);
        }
    }

    public void hurt()
    {
        currHealth--;
        updateHealthBar();
        Debug.Log(currHealth);
    }

    public void heal()
    {
        if(currHealth < maxHealth)
            currHealth++;
        updateHealthBar();
        AudioSource.PlayClipAtPoint(EatSound, transform.position, 1);
        Debug.Log(currHealth);
    }

    protected IEnumerator InvulnerableTimer()
    {
        yield return new WaitForSeconds(timeBeforeNextHit);
        invulnerable = false;
    }

    private void updateHealthBar()
    {
        HpText.text = currHealth.ToString() + "/" + maxHealth.ToString();
        HPSlider.value = (float)currHealth / (float)maxHealth;
    }

}
