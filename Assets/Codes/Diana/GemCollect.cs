using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public enum GemType { Health, GPA, Money, Social };
    public GemType gemType;
    public float increaseAmount = 1f;
    private ProgressBarsControl progressBarControl;

    public AudioClip collectSound;
    private AudioSource audioSource;

    void Start()
    {
        progressBarControl = FindObjectOfType<ProgressBarsControl>();
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 5.0f;
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
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
