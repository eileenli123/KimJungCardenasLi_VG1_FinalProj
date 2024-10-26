using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rigidbody2D;
    public int jumpsLeft;
    private ProgressBarsControl progressBarControl;  // Reference to the progress bar controller
    Animator animator;
    SpriteRenderer sprite;

    private TutorialManager tutorialManager;

    void Start()
    {
        // Find the ProgressBarsControl script in the scene
        progressBarControl = FindObjectOfType<ProgressBarsControl>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();


    }

    //For animation
    void FixedUpdate()
    {
        //This update event is sync'd with the physics engine
        animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
        if (_rigidbody2D.velocity.magnitude > 0)
        {
            animator.speed = _rigidbody2D.velocity.magnitude / 3f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 2.5f;  // Increase gravity to make player fall faster
    }

    // Update is called once per frame
    float maxSpeed = 18f;
    float acceleration = 5f;  // Adjust to control how quickly the player accelerates
    bool isMovingLeft = false;
    bool isMovingRight = false;

    void Update()
    {
        _rigidbody2D.drag = 0f;


        // Handle left movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
            isMovingRight = false;
            sprite.flipX = true;

            // Apply force to the left if under max speed
            if (_rigidbody2D.velocity.x > -maxSpeed)
            {
                _rigidbody2D.AddForce(Vector2.left * acceleration, ForceMode2D.Force);
            }
        }

        // Handle right movement
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isMovingRight = true;
            isMovingLeft = false;
            sprite.flipX = false;

            // Apply force to the right if under max speed
            if (_rigidbody2D.velocity.x < maxSpeed)
            {
                _rigidbody2D.AddForce(Vector2.right * acceleration, ForceMode2D.Force);
            }
        }

        // Stop movement if neither key is pressed
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isMovingLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMovingRight = false;
        }

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 13f);  // Apply upward velocity
            jumpsLeft--;
        }
        animator.SetInteger("JumpsLeft", jumpsLeft);

        // Cap horizontal speed
        if (Mathf.Abs(_rigidbody2D.velocity.x) > maxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * maxSpeed, _rigidbody2D.velocity.y);
        }

        // Cap vertical speed
        if (_rigidbody2D.velocity.y > 18f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 18f);
        }
    }


    // Handle collision with ground
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f);
            Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.red);  // Visualize raycast
            jumpsLeft = 2;
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 2;  // Reset jump count
                }
            }
        }

        // Handle collision with enemy
        if (other.gameObject.CompareTag("Enemy"))  // Check if collided with an enemy
        {
            if (progressBarControl != null)
            {
                // Decrease player's social gems by 1 
                progressBarControl.IncreaseSocial(-1f);
            }
        }
    }
}