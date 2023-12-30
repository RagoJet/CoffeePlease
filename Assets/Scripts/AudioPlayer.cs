using Services;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, IAudioPlayer{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip moneyClip;

    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMoneysSound(){
        _audioSource.PlayOneShot(moneyClip);
    }
}

public interface IAudioPlayer : IService{
    public void PlayMoneysSound();
}