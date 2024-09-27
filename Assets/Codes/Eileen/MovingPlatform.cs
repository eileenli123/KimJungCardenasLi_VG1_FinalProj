using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos; 
    // Start is called before the first frame update
    void Start()
    {
        targetPos = posB.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f)
        {
            targetPos = posB.position; 
        }


        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime); 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform); 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null); 
    }


}
