using UnityEngine;

public class Healing : PowerUp
{
    GameObject player;
    PlayerHealth playerHealth;
    public AudioSource healingAudio;

    public override void Awake()
    {
        //Search for player tag
        player = GameObject.FindGameObjectWithTag("Player");

        //Get PlayerHealth
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        //Set player in range
        float dist = Vector3.Distance(other.transform.position, transform.position);
        if ((other.tag == "Player") && (dist < 1.5))
        {
            playerHealth.Healing();
            this.gameObject.SetActive(false);
        }
    }
}
