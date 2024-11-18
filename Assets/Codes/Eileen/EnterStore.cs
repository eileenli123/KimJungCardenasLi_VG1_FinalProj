using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterStore : MonoBehaviour
{
    private bool enteredOnce = false;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enteredOnce)
        {
            StoreController.instance.show();
            enteredOnce = true;
            boxCollider.enabled = false; //turn off boxCollider so player can proceed 
        }
    }

}
