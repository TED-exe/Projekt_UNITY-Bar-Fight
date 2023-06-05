using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _glassBreakingSounds;
    [SerializeField] private AudioSource _soundsSource;
    [Range(0.1f, 0.5f)]
    [SerializeField] private float _soundsVolumMultiplayer;
    [Range(0.1f, 0.5f)]
    [SerializeField] private float _soundsPitchMultiplayer;
    public void RandomizeGlassBreakingSounds()
    {
        _soundsSource.clip = _glassBreakingSounds[Random.Range(0, _glassBreakingSounds.Length)];
        _soundsSource.volume = Random.Range(1 - _soundsVolumMultiplayer, 1);
        _soundsSource.pitch = Random.Range(1 - _soundsPitchMultiplayer, 1 + _soundsPitchMultiplayer);
        _soundsSource.PlayOneShot(_soundsSource.clip);
    }

    private void Awake()
    {
        RandomizeGlassBreakingSounds();
    }
}
