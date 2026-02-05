using UnityEngine;

public class AudioPlaySmash : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;

    [SerializeField]
    private GameObject _audioPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Attack"))
        {
            Debug.Log("glass");
            AudioClipSetting audio = GameObject.Instantiate(_audioPrefab).GetComponent<AudioClipSetting>();
            audio.SetAudioClip(_clip);
            audio.Play();
            audio.GetComponent<DestroyByTime>().TimerStart();
        }
    }

}
