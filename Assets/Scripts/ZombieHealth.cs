using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public int health = 3;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= (int)damage;
        Debug.Log("Zombie Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Zombie died!");
        animator.SetTrigger("isDead");
        StartCoroutine(DestroyAfterDelay(1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
