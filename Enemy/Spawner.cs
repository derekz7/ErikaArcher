using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;
    [SerializeField]
    private bool respawn;
    [SerializeField]
    private float spawnDelay;

    private float currentTime;
    private bool spawning;


    void Start()
    {
        Spawn();
        currentTime = spawnDelay;
    }
    void Update()
    {
        if (spawning)
        {
            currentTime  -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Spawn();
            }
        }
    }
    public void Respawn()
    {
        spawning = true;
        currentTime = spawnDelay;
    }
    void Spawn()
    {
        IEnemy instance = Instantiate(monster, transform.position, Quaternion.identity).GetComponent<IEnemy>();
        instance.spawner = this;
        spawning = false;
    }
}
