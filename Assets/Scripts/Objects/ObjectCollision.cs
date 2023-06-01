using UnityEngine;
using UnityEngine.Events;

public class ObjectCollision : MonoBehaviour
{
    private const int ENVIRONMENT_LAYER_NUMBER = 6;
    private const int THROWED_LAYER_NUMBER = 7;
    private const int PICKABLE_LAYER_NUMBER = 3;
    [SerializeField] private AudioClip[] _glassBreakingSounds;
    [SerializeField] private AudioSource _soundsSource;
    [Range(0.1f,0.5f)]
    [SerializeField] private float _soundsVolumMultiplayer;
    [Range(0.1f, 0.5f)]
    [SerializeField] private float _soundsPitchMultiplayer;

    public UnityEvent onPlayerHit;
    private void OnCollisionEnter(Collision other)
    {
        CheckDestruction(other);
    }

    private void CheckDestruction(Collision other)
    {
        if (other.gameObject.CompareTag(tag: "Player") && gameObject.layer == THROWED_LAYER_NUMBER)
        {
            onPlayerHit.Invoke();
        }
        else if (other.gameObject.layer == ENVIRONMENT_LAYER_NUMBER && gameObject.layer == THROWED_LAYER_NUMBER)
        {
            gameObject.layer = PICKABLE_LAYER_NUMBER;
        }
    }
    public void RandomizeGlassBreakingSounds()
    {
        _soundsSource.clip = _glassBreakingSounds[Random.Range(0,_glassBreakingSounds.Length)];
        _soundsSource.volume = Random.Range(1 - _soundsVolumMultiplayer, 1);
        _soundsSource.pitch = Random.Range(1 - _soundsPitchMultiplayer, 1 + _soundsPitchMultiplayer);
        _soundsSource.PlayOneShot(_soundsSource.clip);
    }
}
