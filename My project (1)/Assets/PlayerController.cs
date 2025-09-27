using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class TopDownCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;

    // --- NEW VARIABLES FOR ATTACKING ---
    [Header("Attack Settings")]
    public int attackDamage = 25;
    public float attackRange = 2.0f;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // --- MOVEMENT LOGIC (from before) ---
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
        }

        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);
        rb.velocity = speed * dir;


        // --- NEW ATTACK LOGIC ---
        // Check if the attack key is pressed.
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Step 1: Find the GameObject tagged as "Enemy".
            GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");

            // Step 2: Check if an enemy exists in the scene.
            if (enemyObject != null)
            {
                // Step 3: Check if the enemy is within attack range.
                if (Vector2.Distance(transform.position, enemyObject.transform.position) <= attackRange)
                {
                    Debug.Log("Player attacks " + enemyObject.name);

                    // Step 4: Get the Health component FROM THE ENEMY.
                    Health enemyHealth = enemyObject.GetComponent<Health>();

                    // Step 5: If the enemy has a Health component, deal damage.
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(attackDamage);
                    }
                }
            }
        }
    }
}
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(Animator))]
// public class PlayerController : MonoBehaviour
// {
//     [Header("Movement")]
//     public float speed = 5f;

//     [Header("Combat")]
//     public Transform attackPoint;
//     public float attackRange = 1.0f;
//     public LayerMask enemyLayers;
//     public int attackDamage = 1;
//     public float attackPointOffset = 1.0f;

//     private Rigidbody2D rb;
//     private Animator animator;
//     private Vector2 moveDirection;
//     private Vector2 lastMoveDirection;

//     void Awake()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         if (attackPoint == null) Debug.LogError("Attack Point not assigned!", this);
//         lastMoveDirection = Vector2.down;
//     }

//     void Update()
//     {
//         float moveX = Input.GetAxisRaw("Horizontal");
//         float moveY = Input.GetAxisRaw("Vertical");
//         moveDirection = new Vector2(moveX, moveY).normalized;

//         if (moveDirection.magnitude > 0.1f)
//         {
//             lastMoveDirection = moveDirection;
//         }
//         attackPoint.localPosition = lastMoveDirection * attackPointOffset;

//         if (Input.GetButtonDown("Fire1"))
//         {
//             Attack();
//         }
//     }

//     void FixedUpdate()
//     {
//         rb.velocity = moveDirection * speed;
//         UpdateAnimation();
//     }

//     void UpdateAnimation()
//     {
//         animator.SetBool("IsMoving", moveDirection.magnitude > 0);
//         if (moveDirection.magnitude > 0.1f)
//         {
//             animator.SetFloat("MoveX", moveDirection.x);
//             animator.SetFloat("MoveY", moveDirection.y);
//         }
//         else
//         {
//             animator.SetFloat("MoveX", lastMoveDirection.x);
//             animator.SetFloat("MoveY", lastMoveDirection.y);
//         }
//     }

//     void Attack()
//     {
//         animator.SetTrigger("Attack");
//         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
//         foreach(Collider2D enemy in hitEnemies)
//         {
//             Health enemyHealth = enemy.GetComponent<Health>();
//             if (enemyHealth != null)
//             {
//                 enemyHealth.TakeDamage(attackDamage);
//             }
//         }
//     }

//     void OnDrawGizmosSelected()
//     {
//         if (attackPoint == null) return;
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//     }
// }