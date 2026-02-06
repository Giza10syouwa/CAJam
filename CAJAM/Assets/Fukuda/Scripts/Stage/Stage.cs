using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage : MonoBehaviour
{
    //
    [SerializeField]
    private float _maxRatio;

    private float _time;

    [SerializeField]
    private float INITIAL_SPEED;

    private float _speed;

    [SerializeField]
    private string _nextScene;

    [SerializeField]
    private Player _player;
    [SerializeField]
    private Boss _boss;

    [SerializeField]
    private UnityEngine.UI.Image _image;

    private bool _fadeF;

    private bool _fadeOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _time = 0.0f;
        _speed = INITIAL_SPEED;
        _fadeF = false;
        _fadeOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime ;

        if(_time >= 4.0f && !_fadeF)
        {
            _player.CanPlay();
            _boss.CanPlay();
        }

        //_joshi.SetRatio(_time / _maxRatio);

        if(_fadeF)
        {
            _image.color = new Color(0,0,0 ,_image.color.a + Time.deltaTime );
            if(_image.color.a >= 1.0f)
            {
                SceneManager.LoadScene(_nextScene);

            }
        }
        else if(_image.color.a >= 0.0f)
        {

            _image.color = new Color(0, 0, 0, _image.color.a - Time.deltaTime );
        }
        
    }

    public void GameClear()
    {
        Score.Instance.AddScore(-(int)((_time - 4.0f) * 10.0f), "‚¢‚¿‚Í‚â‚­ãi‚ğ‘—‚è“Í‚¯‚È‚¯‚ê‚Î‚È‚ç‚È‚¢");
        _time = 0.0f;
        _fadeF = true;
    }
}
