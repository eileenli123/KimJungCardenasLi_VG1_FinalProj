using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public enum GemType { Health, GPA, Money, Social };
    public GemType gemType;
    public float increaseAmount = 1f;
    private ProgressBarsControl progressBarControl;

    void Start()
    {
        progressBarControl = FindObjectOfType<ProgressBarsControl>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && progressBarControl != null)
        {
            switch (gemType)
            {

                case GemType.GPA:
                    progressBarControl.IncreaseGPA(increaseAmount);
                    break;
                case GemType.Money:
                    progressBarControl.IncreaseCoins((int)increaseAmount);
                    break;
                case GemType.Social:
                    progressBarControl.IncreaseSocial(increaseAmount);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
