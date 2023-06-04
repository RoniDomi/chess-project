using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hover;
    public AudioClip pressed;

    public void onHover()
    {
        audioSource.PlayOneShot(hover);
    }
    public void onClick()
    {
        audioSource.PlayOneShot(pressed);
    }
}
