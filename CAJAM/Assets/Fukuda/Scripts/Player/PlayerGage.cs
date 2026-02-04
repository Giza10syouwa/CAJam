using UnityEngine;

public class PlayerGage : MonoBehaviour
{
    //ゲージのオブジェクト
    [SerializeField]
    private GameObject _gageObj;

    //画像
    private UnityEngine.UI.Image _image;

    //表示位置等
    private RectTransform _rectTransform;

    //有効か
    private bool _enabled;

    //位置
    private Vector3 _position;

    //ゲージのスプライトの配列
    [SerializeField]
    private Sprite[] _sprites;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enabled = false;
        _rectTransform = _gageObj.GetComponent<RectTransform>();
        _image = _gageObj.GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_enabled)
        {
            _rectTransform.position = _position;

        }
    }

    //ゲージの表示非表示
    public void SetGageActive(bool active)
    {
        _gageObj.SetActive(active);
        _enabled = active;
    }

    public void SetPos(Vector3 pos)
    {
        _position = Camera.main.WorldToScreenPoint(pos);
        if(_position.z > 0.0f)
            _position.z = 1.0f;
        if(_position.z <= 0.0f)
            _position.z = -1.0f;
    }

    public void SetPower(int power)
    {
        power = Mathf.Clamp(power, 0, _sprites.Length - 1);
        _image.sprite = _sprites[power];
    }

}
