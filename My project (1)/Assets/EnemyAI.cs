using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform playerTarget;
    public Transform attackPoint;

    [Header("AI Settings")]
    public float moveSpeed = 1.5f;
    public float detectionRange = 6f;
    public float stopChasingRange = 8f;

    [Header("Attack Settings")]
    public int attackDamage = 1;
    public float attackRange = 1f;
    public float attackCooldown = 2f;
    public LayerMask playerLayer;
    public float attackPointOffset = 1.0f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isChasing = false;
    private float nextAttackTime = 0f;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (playerTarget == null) Debug.LogError("Player Target not assigned!", this);
        if (attackPoint == null) Debug.LogError("Attack Point not assigned!", this);
        lastMoveDirection = Vector2.down;
    }

    void FixedUpdate()
    {
        if (playerTarget == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

        if (isChasing)
        {
            if (distanceToPlayer > stopChasingRange) isChasing = false;
        }
        else
        {
            if (distanceToPlayer < detectionRange) isChasing = true;
        }

        if (isChasing)
        {
            moveDirection = (playerTarget.position - transform.position).normalized;
            lastMoveDirection = moveDirection;

            if (distanceToPlayer > attackRange)
            {
                rb.velocity = moveDirection * moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            moveDirection = Vector2.zero;
        }

        attackPoint.localPosition = lastMoveDirection * attackPointOffset;
        UpdateAnimation();
    }
    
    void UpdateAnimation()
    {
        animator.SetBool("IsMoving", moveDirection.magnitude > 0);
        animator.SetFloat("MoveX", lastMoveDirection.x);
        animator.SetFloat("MoveY", lastMoveDirection.y);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach(Collider2D player in hitPlayers)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}