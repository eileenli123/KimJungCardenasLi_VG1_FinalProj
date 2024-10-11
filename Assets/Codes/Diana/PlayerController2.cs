using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rigidbody2D;
    public int jumpsLeft;
    private ProgressBarsControl progressBarControl;  // Reference to the progress bar controller

    void Start()
    {
        // Find the ProgressBarsControl script in the scene
        progressBarControl = FindObjectOfType<ProgressBarsControl>();
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move player left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
        }
        
        // Move player right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
        }

        // Jump 
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            _rigidbody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
            jumpsLeft--;
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