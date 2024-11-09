using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Tutorial : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public int jumpsLeft = 2;
    private ProgressBarsControl progressBarControl;
    Animator animator;
    SpriteRenderer sprite;
    private TutorialManager tutorialManager;

    public LayerMask groundLayer;  // Assign in Inspector for ground detection
    public float maxSpeed = 18f;
    public float acceleration = 5f;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        progressBarControl = FindObjectOfType<ProgressBarsControl>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
        animator.speed = _rigidbody2D.velocity.magnitude > 0 ? _rigidbody2D.velocity.magnitude / 3f : 1f;
    }

    void Update()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);
        if (isGrounded)
        {
            jumpsLeft = 2;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
            sprite.flipX = true;
            ApplyHorizontalForce(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isMovingRight = true;
            sprite.flipX = false;
            ApplyHorizontalForce(Vector2.right);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) isMovingLeft = false;
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) isMovingRight = false;

        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 13f);
            jumpsLeft--;
        }

        animator.SetInteger("JumpsLeft", jumpsLeft);
    }

    private void ApplyHorizontalForce(Vector2 direction)
    {
        if (Mathf.Abs(_rigidbody2D.velocity.x) < maxSpeed)
        {
            _rigidbody2D.AddForce(direction * acceleration, ForceMode2D.Force);
        }
        _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -maxSpeed, maxSpeed), _rigidbody2D.velocity.y);
    }


}
