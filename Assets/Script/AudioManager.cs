using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        if (!_audioSource.isPlaying)
        {
            var audioClip = Resources.Load<AudioClip>("Sound/" + name);
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    }

}
