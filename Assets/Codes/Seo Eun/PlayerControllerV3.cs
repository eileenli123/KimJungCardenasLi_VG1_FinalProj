using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV3 : MonoBehaviour
{
    // ============================
    // Components and References
    // ============================
    private Rigidbody2D _rigidbody2D;
    private Animator animator;
    private SpriteRenderer sprite;
    private ProgressBarsControl progressBarControl;  // Reference to the progress bar controller
    private TutorialManager tutorialManager;

    // ============================
    // Movement Parameters
    // ============================
    [Header("Movement Parameters")]
    public float maxSpeed = 18f;
    public float acceleration = 5f;  // Controls how quickly the player accelerates
    public float jumpVelocity = 13f;  // Upward velocity when jumping
    public int maxJumps = 2;          // Maximum number of jumps (for double jump)

    // ============================
    // Climbing Parameters
    // ============================
    [Header("Climbing Parameters")]
    public float climbSpeed = 4f;         // Speed of climbing
    public LayerMask climbableLayer;      // Layer mask for climbable surfaces
    public Transform climbCheck;          // Empty GameObject positioned to check climbable surfaces
    public float climbCheckRadius = 0.5f; // Radius for climbable detection

    // ============================
    // Ground Check Parameters
    // ============================
    [Header("Ground Check")]
    public Transform groundCheck;          // Empty GameObject positioned at the player's feet
    public float groundCheckRadius = 0.2f;// Radius for ground check overlap
    public LayerMask groundLayer;          // Layer mask for ground detection

    // ============================
    // State Variables
    // ============================
    private int jumpsLeft;
    private bool isClimbing = false;
    private bool isTouchingClimbable = false;

    // Movement State Variables
    private float moveInput = 0f; // -1 for left, 1 for right, 0 for no input

    void Awake()
    {
        // Initialize components
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        // Set initial gravity scale
        _rigidbody2D.gravityScale = 2.5f;  // Increase gravity to make player fall faster
    }

    void Start()
    {
        // Find the ProgressBarsControl script in the scene
        progressBarControl = FindObjectOfType<ProgressBarsControl>();

        // Initialize jumpsLeft
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        DetectClimbable();
        HandleClimbingInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleClimbing();
        UpdateAnimator();
    }

    /// <summary>
    /// Handles horizontal movement input (left/right).
    /// </summary>
    private void HandleMovementInput()
    {
        // Reset moveInput each frame
        moveInput = 0f;

        // Check for left movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
            sprite.flipX = true;
            Debug.Log("Moving Left");
        }
        // Check for right movement
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
            sprite.flipX = false;
            Debug.Log("Moving Right");
        }
    }

    /// <summary>
    /// Handles jump input.
    /// </summary>
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0 && !isClimbing)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);  // Apply upward velocity
            jumpsLeft--;
            Debug.Log($"Jumped! Jumps left: {jumpsLeft}");
        }
    }

    /// <summary>
    /// Detects if the player is touching a climbable surface.
    /// </summary>
    private void DetectClimbable()
    {
        if (climbCheck == null)
        {
            Debug.LogError("ClimbCheck Transform is not assigned in the Inspector.");
            return;
        }

        Collider2D hit = Physics2D.OverlapCircle(climbCheck.position, climbCheckRadius, climbableLayer);
        isTouchingClimbable = hit != null;

        if (isTouchingClimbable)
        {
            Debug.Log("Touching Climbable Surface");
        }
    }

    /// <summary>
    /// Handles climbing input and state transitions.
    /// </summary>
    private void HandleClimbingInput()
    {
        if (isTouchingClimbable && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (!isClimbing)
            {
                isClimbing = true;
                _rigidbody2D.gravityScale = 0f;  // Disable gravity while climbing
                Debug.Log("Climbing Started");
            }
        }
        else
        {
            if (isClimbing && !IsGrounded())
            {
                isClimbing = false;
                _rigidbody2D.gravityScale = 2.5f;  // Restore gravity
                Debug.Log("Climbing Ended");
            }
        }
    }

    /// <summary>
    /// Handles horizontal and vertical movement, including climbing.
    /// </summary>
    private void HandleMovement()
    {
        if (!isClimbing)
        {
            // Apply horizontal force based on input
            if (moveInput != 0)
            {
                // Determine direction
                Vector2 direction = moveInput > 0 ? Vector2.right : Vector2.left;

                // Apply force if under max speed
                if ((_rigidbody2D.velocity.x < maxSpeed && moveInput > 0) ||
                    (_rigidbody2D.velocity.x > -maxSpeed && moveInput < 0))
                {
                    _rigidbody2D.AddForce(direction * acceleration, ForceMode2D.Force);
                }
            }
        }

        // Cap horizontal speed
        if (Mathf.Abs(_rigidbody2D.velocity.x) > maxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * maxSpeed, _rigidbody2D.velocity.y);
            Debug.Log($"Horizontal speed capped to {maxSpeed}");
        }

        // Cap vertical speed
        if (_rigidbody2D.velocity.y > 18f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 18f);
            Debug.Log("Vertical speed capped to 18");
        }
        else if (_rigidbody2D.velocity.y < -18f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -18f);
            Debug.Log("Vertical speed capped to -18");
        }
    }

    /// <summary>
    /// Handles climbing movement.
    /// </summary>
    private void HandleClimbing()
    {
        if (isClimbing)
        {
            float verticalInput = 0f;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                verticalInput = 1f;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                verticalInput = -1f;
            }

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, verticalInput * climbSpeed);
            Debug.Log($"Climbing with vertical input: {verticalInput}");
        }
    }

    /// <summary>
    /// Updates animator parameters based on movement and climbing state.
    /// </summary>
    private void UpdateAnimator()
    {
        // Update Speed parameter
        float speed = Mathf.Abs(_rigidbody2D.velocity.x) + Mathf.Abs(_rigidbody2D.velocity.y);
        animator.SetFloat("Speed", speed);

        // Adjust animator speed based on velocity magnitude
        if (_rigidbody2D.velocity.magnitude > 0)
        {
            animator.speed = Mathf.Clamp(_rigidbody2D.velocity.magnitude / 3f, 1f, 3f);
        }
        else
        {
            animator.speed = 1f;
        }

        // Update JumpsLeft parameter
        animator.SetInteger("JumpsLeft", jumpsLeft);

        // Update Climbing state
        animator.SetBool("IsClimbing", isClimbing);
    }

    /// <summary>
    /// Checks if the player is grounded by casting a circle at the groundCheck position.
    /// </summary>
    /// <returns>True if grounded, otherwise false.</returns>
    private bool IsGrounded()
    {
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck Transform is not assigned in the Inspector.");
            return false;
        }

        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // Optional: Visual confirmation
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckRadius, Color.red);
        return hit != null;
    }

    /// <summary>
    /// Handles collision with ground and enemies.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Handle collision with ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f, groundLayer);
            Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.red);  // Visualize raycast
            jumpsLeft = maxJumps;  // Reset jump count to maxJumps

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = maxJumps;  // Ensure jump count is reset
                    break;  // Exit loop once ground is detected
                }
            }

            Debug.Log($"Collided with Ground. Jumps left: {jumpsLeft}");
        }

        // Handle collision with enemy
        if (other.gameObject.CompareTag("Enemy"))  // Check if collided with an enemy
        {
            if (progressBarControl != null)
            {
                // Decrease player's social gems by 1 
                progressBarControl.IncreaseSocial(-1f);
                Debug.Log("Collided with Enemy. Social gems decreased.");
            }
        }
    }

    /// <summary>
    /// Visualize climbable and ground detection areas in the editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Draw climbable check circle
        if (climbCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(climbCheck.position, climbCheckRadius);
        }

        // Draw ground check circle
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}