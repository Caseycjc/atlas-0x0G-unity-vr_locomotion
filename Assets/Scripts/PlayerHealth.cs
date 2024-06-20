using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public delegate void HealthChanged(int health);
    public event HealthChanged OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth); // Notify listeners

        if (currentHealth <= 0)
        {
            LoseGame();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth); // Notify listeners
    }

    void LoseGame()
    {
        // Load lose screen
        SceneManager.LoadScene("LoseScreen");
    }
}
