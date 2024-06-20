using UnityEngine;

public class BroomAttack : MonoBehaviour
{
    public float damage = 1f;
    public float pushBackForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                Debug.Log("Zombie hit! Applying damage.");
                zombieHealth.TakeDamage(damage);

                // Apply a push back force
                Rigidbody zombieRigidbody = other.GetComponent<Rigidbody>();
                if (zombieRigidbody != null)
                {
                    Vector3 direction = other.transform.position - transform.position;
                    direction.y = 0; // Keep the force horizontal
                    zombieRigidbody.AddForce(direction.normalized * pushBackForce, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.LogWarning("ZombieHealth component not found on the zombie!");
            }
        }
    }
}
