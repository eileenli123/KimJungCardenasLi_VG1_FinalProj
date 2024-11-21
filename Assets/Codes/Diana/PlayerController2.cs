using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public static PlayerController2 instance;

    // Outlet
    Rigidbody2D _rigidbody2D;
    public int jumpsLeft;
    private ProgressBarsControl progressBarControl;  // Reference to the progress bar controller
    Animator animator;
    SpriteRenderer sprite;
    public bool isPaused = false;




    void Awake()
    {
        instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        // Find the ProgressBarsControl script in the scene
        progressBarControl = FindObjectOfType<ProgressBarsControl>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();



    }

    //For animation
    void FixedUpdate()
    {
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
    private void OnEnable()
    {
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            /*             _rigidbody2D.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
             */
            _rigidbody2D.velocity = new Vector2(-7f, _rigidbody2D.velocity.y);

            sprite.flipX = true;
        }


        // Move player right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            /*             _rigidbody2D.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
             */
            _rigidbody2D.velocity = new Vector2(7f, _rigidbody2D.velocity.y);

            sprite.flipX = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));


        // Jump 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsLeft > 0)
            {
                jumpsLeft--;
                /* _rigidbody2D.AddForce(Vector2.up * 12f, ForceMode2D.Impulse); */
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 23f); // Use velocity for controlled jump
            }
        }

        animator.SetInteger("JumpsLeft", jumpsLeft);


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
    public void PausePlayer()
    {
        _rigidbody2D.velocity = Vector2.zero;
        enabled = false;
        animator.SetFloat("Speed", 0);
    }

    public void ResumePlayer()
    {
        enabled = true;
        jumpsLeft = 2;
        if (_rigidbody2D.velocity.magnitude > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
        }
        else
        {
            animator.SetFloat("Speed", 0.1f);
        }

    }
}
