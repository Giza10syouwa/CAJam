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

    //アニメーション
    [SerializeField]
    private Animator _anim;

    //プレイヤーのモデルの回転
    [SerializeField]
    private Transform _modelRot;

    private Vector3 _lastDirection;

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

        _anim.SetFloat("Speed", direction.magnitude);

        //デバッグ用に
        d_direction = direction;

        //XZ平面上の進行方向をきめる
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.y);

        //現在のモデルの向く方向をきめる
        Vector3 currentModelDirection = Vector3.Lerp(_lastDirection, directionXZ,Time.deltaTime * 5.0f);

        //モデルの回転
        _modelRot.localRotation = Quaternion.LookRotation(currentModelDirection);

        _lastDirection = currentModelDirection;

        //入力方向とスピードから速度取得
        Vector3 velocity = directionXZ * _speed * Time.deltaTime;

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
