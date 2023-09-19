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

    // fungsi untuk menambah nyawa
    public void Healing()
    {
        //mengurangi health
        int newHealth = currentHealth + 40;
        if (newHealth >= 100)
        {
            currentHealth = 100;
        }
        else
        {
            currentHealth = newHealth;
        }

        //Merubah tampilan dari health slider
        healthSlider.value = currentHealth;
    }


    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        //mentrigger animasi Die
        anim.SetTrigger("Die");

        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //mematikan script player movement
        playerMovement.enabled = false;

        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }
}
