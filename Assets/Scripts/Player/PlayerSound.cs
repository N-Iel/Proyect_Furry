using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource audioS;

    [Header("Movement")]
    public AudioClip[] step;
    public AudioClip[] dash;

    float originalPitch;

    private void Start()
    {
        originalPitch = audioS.pitch;
    }

    public void PlayStep()
    {
        ResetPitch();
        audioS.clip = step[Random.Range(0, step.Length)];
        audioS.pitch = Random.Range(0.95f, 1.05f);
        audioS.Play();
    }

    public void PlayDash()
    {
        ResetPitch();
        audioS.clip = dash[Random.Range(0, dash.Length)];
        audioS.Play();
    }

    public void ResetPitch()
    {
        audioS.pitch = originalPitch;
    }
}
