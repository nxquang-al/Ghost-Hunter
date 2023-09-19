using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 7f;

    [SerializeField]
    PowerUpFactory factory;
    IFactory Factory
    {
        get
        {
            return factory as IFactory;
        }
    }

    void Start()
    {
        //Execute the Spawn function every few seconds according to the spawnTime value
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        //If player died, then no new enemies are created
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //Create a new randomed enemy
        int spawnPoint = Random.Range(0, 3);
        Factory.FactoryMethod(spawnPoint);
    }
}