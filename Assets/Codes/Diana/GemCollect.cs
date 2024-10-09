using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public enum GemType { Health, GPA, Money, Social };
    public GemType gemType; 
    public float increaseAmount = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ProgressBarsControl progressBarControl = FindObjectOfType<ProgressBarsControl>();

            if (progressBarControl != null)
            {
                switch (gemType)
                {
                    case GemType.Health:
                        progressBarControl.IncreaseHealth(increaseAmount); 
                        break;
                    case GemType.GPA:
                        progressBarControl.IncreaseGPA(increaseAmount); 
                        break;
                    case GemType.Money:
                        progressBarControl.IncreaseMoney(increaseAmount); 
                        break;
                    case GemType.Social:
                        progressBarControl.IncreaseSocial(increaseAmount); 
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}