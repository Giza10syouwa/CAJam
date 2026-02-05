using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //çUåÇóÕ
    private int _power;

    private Vector3 _smashDirection;

    [SerializeField]
    private ParticleSystem _effect;

    [SerializeField]
    private AudioClip[] _clips;

    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _audioPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        if(_effect)
        {

            AudioClipSetting audioObject = GameObject.Instantiate(_audioPrefab).GetComponent<AudioClipSetting>();
            AudioSource src = audioObject.GetComponent<AudioSource>(); // å≥ÇÃ AudioSource ÇÃê›íËÇÉRÉsÅ[
            src.volume = _audioSource.volume;
            src.pitch = _audioSource.pitch;
            src.spatialBlend = _audioSource.spatialBlend;
            src.loop = _audioSource.loop;
            src.playOnAwake = _audioSource.playOnAwake;
            src.minDistance = _audioSource.minDistance;
            src.maxDistance = _audioSource.maxDistance;
            src.rolloffMode = _audioSource.rolloffMode;
            //----------------------------------------
            audioObject.SetAudioClip(_clips[0]);
            audioObject.Play();
            
            _effect.Play();
        }
    }

    public int GetPower()
    {
        return _power;
    }

    public void SetPower(int power)
    {
        _power = Mathf.Clamp(power,0,5);
        
    }

    public Vector3 GetSmashDirection()
    {
        return _smashDirection;
    }
    public void SetSmashDirection(Vector3 direction)
    {
        _smashDirection = direction;
    }

    public void AddEffectScale(float scale)
    {
        if(_effect)
        {
            _effect.gameObject.transform.localScale += new Vector3(scale, scale, scale);
        }
    }
    public void SetEffectScale(float scale)
    {
        if (_effect)
        {
            _effect.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }


}
