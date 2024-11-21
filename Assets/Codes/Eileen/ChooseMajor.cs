using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseMajor : MonoBehaviour
{
    public GameObject choice1;
    public GameObject choice2;
    public GameObject choice3;
    public GameObject choice4;
    public ProgressBarsControl progressBarsControl;
    public PlayerController_Levels playerController;

    private void Start()
    {
        // to avoid nullRef 
        if (choice1 == null || choice2 == null || choice3 == null || choice4 == null || progressBarsControl)
        {
            Debug.LogError("choice game obj has null public obj");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {


                // Check which GameObject was clicked
                if (hit.collider.gameObject == choice1)
                {
                    progressBarsControl.setMajor(2.0f, "Communications");
                    playerController.ResumePlayer();



                }
                else if (hit.collider.gameObject == choice2)
                {
                    progressBarsControl.setMajor(2.5f, "Business");
                    playerController.ResumePlayer();



                }
                else if (hit.collider.gameObject == choice3)
                {
                    progressBarsControl.setMajor(3.0f, "Computer Science");
                    playerController.ResumePlayer();



                }
                else if (hit.collider.gameObject == choice4)
                {
                    progressBarsControl.setMajor(3.5f, "Pre-Med");
                    playerController.ResumePlayer();



                }


            }
        }
    }

}
