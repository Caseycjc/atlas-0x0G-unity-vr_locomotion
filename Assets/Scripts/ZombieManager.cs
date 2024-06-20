using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // Array to hold multiple zombie prefabs
    public Transform[] spawnPoints; // Array to hold the corresponding spawn points
    public Transform player; // Reference to the player Transform

    public void RespawnZombies()
    {
        Debug.Log("RespawnZombies called");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i < zombiePrefabs.Length)
            {
                Debug.Log("Spawning zombie at: " + spawnPoints[i].position);
                GameObject zombie = Instantiate(zombiePrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
                Debug.Log("Zombie instantiated: " + zombie.name);

                // Initialize the AI script
                ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();
                if (zombieAI != null)
                {
                    zombieAI.player = player;
                    Debug.Log("ZombieAI initialized for " + zombie.name);
                }
                else
                {
                    Debug.LogError("ZombieAI script not found on " + zombie.name);
                }
            }
            else
            {
                Debug.LogWarning("There are more spawn points than zombies.");
            }
        }
    }
}
