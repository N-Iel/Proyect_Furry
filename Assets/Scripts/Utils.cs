using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils utils;
    private void Awake()
    {
        utils = this;
    }

    // Round the num passed as param to 2 decimals
    public float TwoDecialRound(float _num)
    {
        return Mathf.Round(_num * 100f) / 100f;
    }

    public void ResetPitch(AudioSource audioS, float _pitch)
    {
        audioS.pitch = _pitch;
    }
}
