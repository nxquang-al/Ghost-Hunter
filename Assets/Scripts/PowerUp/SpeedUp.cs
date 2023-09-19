using UnityEngine;

public class SpeedUp : PowerUp
{
    GameObject player;
    PlayerMovement playerMovement;
    public AudioSource speedUpAudio;

    public override void Awake()
    {
        //Search for player tag
        player = GameObject.FindGameObjectWithTag("Player");

        //Get PlayerMovement
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        //Set player in range
        float dist = Vector3.Distance(other.transform.position, transform.position);
        if ((other.tag == "Player") && (dist < 1.5))
        {
            playerMovement.SpeedUp();
            this.gameObject.SetActive(false);
        }
    }
}
