using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //Initial health
        currentHealth = startingHealth;
    }


    void Update()
    {
        //Check if enemy is sinking
        if (isSinking)
        {
            // Move the enemy down
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //Check if dead
        if (isDead)
            return;

        //play audio
        enemyAudio.Play();

        //Decrease its health
        currentHealth -= amount;    

        //Change the position of the particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();

        //Set its state to death if health <= 0
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        //Sinking after death
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //Set rigidbody to kimematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}