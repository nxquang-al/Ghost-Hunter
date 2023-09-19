using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 3f;

    [SerializeField]
    EnemyFactory factory;
    IFactory Factory
    {
        get
        {
            return factory as IFactory;
        }
    }

    void Start()
    {
        //Spawn after few seconds according to the spawnTime value
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        //If player died then do not create new enemies
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //Create a new randome-type enemy
        int spawnEnemy = Random.Range(0, 3);
        Factory.FactoryMethod(spawnEnemy);
    }
}