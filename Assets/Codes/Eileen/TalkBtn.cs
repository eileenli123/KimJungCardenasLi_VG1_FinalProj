using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBtn : MonoBehaviour
{

    public GameObject GPA_gem_prefab;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.9f)
                {
                    //create social gem object 
                    GameObject newGPAgem = Instantiate(GPA_gem_prefab);
                    Vector3 gemPosition = transform.position;
                    gemPosition.y += 1f;  // Move the gem up so it doesn't collect right away
                    newGPAgem.transform.position = gemPosition;

                    Destroy(gameObject);

                }
            }
        }
    }

   
}
