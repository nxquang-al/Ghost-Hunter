using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            //Fade out damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    //Be attacked logic
    public void TakeDamage(int amount)
    {
        damaged = true;

        //decrease health
        currentHealth -= amount;

        //Update health slider
        healthSlider.value = currentHealth;

        //audio effect
        playerAudio.Play();

        //Invoke death effect once health is less than 0
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    // Increase the health after collecting Health Booster item
    public void Healing()
    {
        //increase by 40 units
        int newHealth = currentHealth + 40;
        if (newHealth >= 100)
        {
            currentHealth = 100;
        }
        else
        {
            currentHealth = newHealth;
        }

        //update the value of the health slider
        healthSlider.value = currentHealth;
    }


    void Death()
    {
        // Player death effect
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //Restart the gameplay after death
        SceneManager.LoadScene(0);
    }
}
