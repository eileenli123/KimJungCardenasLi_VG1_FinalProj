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

    private TutorialManager tutorialManager;

    void Start()
    {
        // Find the ProgressBarsControl script in the scene
        progressBarControl = FindObjectOfType<ProgressBarsControl>();
        animator = GetComponent<Animator>();
        tutorialManager = FindObjectOfType<TutorialManager>();


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
    void Update()
    {
        // Check if the player is allowed to move
        if (tutorialManager != null && !tutorialManager.canPlayerMove)
        {
            return;  // Exit update if the player is not allowed to move yet
        }
        // Move player left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody2D.AddForce(Vector2.left * 15f * Time.deltaTime, ForceMode2D.Impulse);
        }

        // Move player right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody2D.AddForce(Vector2.right * 15f * Time.deltaTime, ForceMode2D.Impulse);
        }

        // Jump 
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            _rigidbody2D.AddForce(Vector2.up * 13f, ForceMode2D.Impulse);
            jumpsLeft--;
        }
        animator.SetInteger("JumpsLeft", jumpsLeft);

        // Limit the player's horizontal velocity (to avoid character from accelerating too fast)
        if (Mathf.Abs(_rigidbody2D.velocity.x) > 18f)  // Set max horizontal speed
        {
            _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * 18f, _rigidbody2D.velocity.y);
        }

        // Limit the player's upward velocity
        if (_rigidbody2D.velocity.y > 18f)  // Set max vertical speed
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