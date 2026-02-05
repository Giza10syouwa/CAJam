using UnityEngine;

public class AudioClipSetting : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAudioClip(AudioClip clip)
    {
        _source.clip = clip;
    }

    public void Play()
    {
        _source.Play();
    }
}
