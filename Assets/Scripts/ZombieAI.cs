using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float walkSpeed = 1f;
    public float runSpeed = 3f;
    public float attackCooldown = 1.5f;

    private Animator animator;
    private Vector3 initialPosition;
    private bool movingTowardsInitial = true;
    private float lastAttackTime = 0;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Freeze unnecessary rotations and vertical position
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        initialPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, rb.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            RunTowardsPlayer();
        }
        else
        {
            Patrol();
        }

        // Enforce constraints every frame to ensure stability
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Patrol()
    {
        Vector3 targetPosition = movingTowardsInitial ? initialPosition : initialPosition + transform.forward * 10; // Adjust the patrol range
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, walkSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        // Calculate direction and ensure it is not zero
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * walkSpeed);
        }

        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);

        if (Vector3.Distance(rb.position, targetPosition) < 0.5f)
        {
            movingTowardsInitial = !movingTowardsInitial;
            transform.Rotate(0, 180, 0); // Turn around
        }
    }



    void RunTowardsPlayer()
    {
        Vector3 newPosition = Vector3.MoveTowards(rb.position, player.position, runSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
        transform.LookAt(player.position);
        animator.SetBool("isRunning", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetBool("isAttacking", true);
            lastAttackTime = Time.time;

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
