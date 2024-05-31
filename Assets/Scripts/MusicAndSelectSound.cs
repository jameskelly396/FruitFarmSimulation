using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAndSelectSound : MonoBehaviour
{
    /* MusicAndSelectSound.cs
     * By: James Kelly
     * Purpose: plays background music and sound effect when mouse button is clicked
     */
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource audioSelectSound;
    private void Awake()
    {
        backgroundMusic.Play();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            audioSelectSound.Play();
        }
    }
}
