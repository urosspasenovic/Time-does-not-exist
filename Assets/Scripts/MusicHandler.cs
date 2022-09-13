using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource background;

    AudioSource audioSource;
    private void Awake()
    {
       audioSource = GetComponent<AudioSource>();
        audioSource.Play(); 
    }
}
