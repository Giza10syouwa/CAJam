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

    //Attackコライダー
    [SerializeField]
    private GameObject _attackColl;
    private PlayerAttack _playerAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = INITIAL_SPEED;
        _playerInput = GetComponent<PlayerInput>().currentActionMap;
        _rb = GetComponent<Rigidbody>();
        _playerAttack = _attackColl.GetComponent<PlayerAttack>();
        ChangeState(PlayerStateID.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        //現在の状態の更新
        _currentState.StateUpdate();

        _canBack = true;
        
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        // "Run" アニメーションが終了したか
        if (_anim.GetInteger("PlayerState") == 3 && info.IsName("Armature|idle"))
        {
            ChangeState(PlayerStateID.Idle);
        }
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

    public GameObject GetAttackColl()
    {
        return _attackColl;
    }

    public int GetPower()
    {
        return _playerAttack.GetPower();
    }

    public void SetPower(int power)
    {
        _playerAttack.SetPower(power);
    }

    public void OnCollisionStay(Collision collision)
    {
        //後ろの壁とあたっている間は後ろへの移動を禁止
        if(collision.gameObject.CompareTag("BackWall"))
        {
            _canBack = false;
        }
    }

    public Animator GetAnimator()
    {
        return _anim;
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

    public Vector3 GetLastDirection()
    {
        return new Vector3(_lastDirection.x,_lastDirection.y,_lastDirection.z);
    }
    public void SetLastDirection(Vector3 lastDirection)
    {
        _lastDirection = lastDirection;
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

}
