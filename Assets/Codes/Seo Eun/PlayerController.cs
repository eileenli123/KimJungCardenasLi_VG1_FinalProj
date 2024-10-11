using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //outlet
    Rigidbody2D _rigidbody2D;
    ///public Transform aimPivot;
    public int jumpsLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake(){
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move player left
        if(Input.GetKey(KeyCode.A)){
            _rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            _rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
        }
        //move player right
        if(Input.GetKey(KeyCode.D)){
            _rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
        }
        
        if(Input.GetKey(KeyCode.RightArrow)){
            _rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
        }

        // Jump 
        if(Input.GetKeyDown(KeyCode.Space)){
            if(jumpsLeft > 0){
                _rigidbody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                jumpsLeft--;
            }
        }

        
    }

    void OnCollisionEnter2D(Collision2D other){
        //check we collided with ground
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f);
            Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.red); //visualize raycast
            jumpsLeft = 2;
            for(int i = 0; i < hits.Length; i++){

                RaycastHit2D hit = hits[i];

                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    //reset jump count
                    jumpsLeft = 2;
                }
            }
        }
    }
}
