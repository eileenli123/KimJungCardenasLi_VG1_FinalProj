using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // code from tutorial : not working
    //Will be fixed by Seo-Eun
    [SerializeField] private float speed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            _rb.velocity = new Vector2(-speed, 0); 
        }

        if (Input.GetKey(KeyCode.RightArrow)) { //continue running as key is held 
            _rb.velocity = new Vector2(speed, 0);
        }


        if (Input.GetKeyDown(KeyCode.Space)) { //only jump once when space hit
            //_rb.velocity.x so that player can move left or right while jumping
            //TO FIX: block double jumping
            _rb.velocity = new Vector2(_rb.velocity.x, 10f);
        }



    }
}
