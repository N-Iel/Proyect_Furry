using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{

    public AudioSource audioS;
    public AudioClip[] attackClips;

    float originalPitch;

    // Start is called before the first frame update
    void Start()
    {
        originalPitch = audioS.pitch;
    }

    public void PlayAttack()
    {
        Utils.utils.ResetPitch(audioS, originalPitch);
        audioS.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioS.pitch = Random.Range(0.95f, 1.05f);
        audioS.Play();
    }
}
