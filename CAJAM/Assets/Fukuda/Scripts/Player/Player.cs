using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum PlayerStateID
    {
        None = -1,
        Idle,
        Move,
        Chage,
        Attack
    };

    private PlayerStateID _currentStateID;

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

    private bool _isAttack;

    private PlayerState _currentState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = INITIAL_SPEED;
        _playerInput = GetComponent<PlayerInput>().currentActionMap;
        _rb = GetComponent<Rigidbody>();
        ChangeState(PlayerStateID.Idle);
    }

    // Update is called once per frame
    void Update()
    {

        ////入力から進行方向を取得
        //Vector2 direction = _playerInput["Move"].ReadValue<Vector2>();
        //direction = new Vector2(direction.x, direction.y);
        ////もし後ろに下がったらダメならマイナスへの移動をなくす
        //if (!_canBack)
        //{
        //    direction.y = Mathf.Clamp(direction.y, 0.0f, 1.0f);
        //}
        ////正規化
        //direction.Normalize();

        ////_anim.SetFloat("Speed", direction.magnitude);

        ////デバッグ用に
        //d_direction = direction;

        _currentState.StateUpdate();

        _canBack = true;

    }

    public void SetSpeed(float speed)
    { 
        _speed = speed;
    }
    public float GetSpeed()
    {
        return _speed;
    }

    public void ChangeState(PlayerStateID id)
    {
        //指定されたIDに合わせて状態を遷移させる
        _currentStateID = id;

        _anim.SetInteger("PlayerState", (int)_currentStateID);


        if (id == PlayerStateID.Idle)
        {
            SetState(new PlayerIdleState(), this);
        }
        if (id == PlayerStateID.Move)
        {
            SetState(new PlayerMoveState(), this);
        }
        if (id == PlayerStateID.Chage)
        {
            SetState(new PlayerChageState(), this);
        }
        if (id == PlayerStateID.Attack)
        {
            SetState(new PlayerAttackState(), this);
        }

    }


    public void OnCollisionStay(Collision collision)
    {
        //後ろの壁とあたっている間は後ろへの移動を禁止
        if(collision.gameObject.CompareTag("BackWall"))
        {
            _canBack = false;
        }
    }

    public Vector2 GetInputMove()
    {
        //入力から進行方向を取得
        Vector2 direction = _playerInput["Move"].ReadValue<Vector2>();
        return new Vector2(direction.x, direction.y);
    }

    public bool GetCanBack()
    {
        return _canBack;
    }

    public void SetState(PlayerState playerState, Player obj)
    {
        if (_currentState != null)
        {
            GameObject.Destroy(_currentState);
        }
        _currentState = playerState;
        _currentState.Initialize(obj);
    }

    public void SetModelRotation(Quaternion rot)
    {
        _modelRot.localRotation = rot;
    }

   
    public void PlayerMove(Vector3 velocity)
    {
        transform.position += velocity;
    }

    public void PlayerAttackChage(Vector3 direction)
    {

    }

    public void PlayerAttack()
    {

    }
}
