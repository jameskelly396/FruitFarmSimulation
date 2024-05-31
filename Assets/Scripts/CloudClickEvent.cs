using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloudClickEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /* CloudClickEvent.cs
     * By: James Kelly
     * Purpose: Clicking on the cloud will cause it to rain partile effects and play audio.
     *          Additionally will make cloud raining flag which is used in SeedGrowth.cs
     */
    private ParticleSystem rainParticleSystem;
    public bool isRaining;
    private AudioSource audioRain;
    private void Awake()
    {
        audioRain = GetComponent<AudioSource>();
        rainParticleSystem = GetComponentInChildren<ParticleSystem>();
        isRaining = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rainParticleSystem.Play();
        isRaining = true;
        audioRain.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rainParticleSystem.Stop();
        isRaining = false;
        audioRain.Pause();
    }
}
