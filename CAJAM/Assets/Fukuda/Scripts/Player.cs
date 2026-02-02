using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //初期速度
    [SerializeField]
    private float INITIAL_SPEED;

    //スピード
    private float _speed;

    //入力
    [SerializeField]
    private InputActionMap _playerInput;

    //リジットボディ
    private Rigidbody _rb;

    //後ろに下がれるか
    private bool _canBack;

    //デバッグ用
    [SerializeField]
    private Vector3 d_direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = INITIAL_SPEED;
        _playerInput = GetComponent<PlayerInput>().currentActionMap;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //入力から進行方向を取得
        Vector2 direction = _playerInput["Move"].ReadValue<Vector2>();
        direction = new Vector2(direction.x, direction.y);
        //もし後ろに下がったらダメならマイナスへの移動をなくす
        if (!_canBack)
        {
            direction.y = Mathf.Clamp(direction.y, 0.0f, 1.0f);
        }
        //正規化
        direction.Normalize();

        //デバッグ用に
        d_direction = direction;

        //入力方向とスピードから速度取得
        Vector3 velocity = new Vector3(direction.x, 0, direction.y) * _speed * Time.deltaTime;

        transform.position += velocity;

        _canBack = true;
    }

    public void SetSpeed(float speed)
    { 
        _speed = speed;
    }

    public void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("BackWall"))
        {
            _canBack = false;
        }
    }
}
