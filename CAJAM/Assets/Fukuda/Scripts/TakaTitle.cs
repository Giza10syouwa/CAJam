using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TakaTitle : MonoBehaviour
{

    [SerializeField]
    private string _nextScene;

    private AudioSource _audioSource;

    private bool _isPlaying;

    [SerializeField]
    private AudioSource _bgm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPlaying )
        {
            
            if(!_audioSource.isPlaying)
            SceneManager.LoadScene(_nextScene);

            _bgm.volume -= Time.deltaTime / 3.0f;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _audioSource.Play();
            _isPlaying = true;
        }
    }
}
