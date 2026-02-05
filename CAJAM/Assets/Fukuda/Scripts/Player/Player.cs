using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;
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
    //後ろに下がれるか
    private bool _canFront;
    //後ろに下がれるか
    private bool _canRight;
    //後ろに下がれるか
    private bool _canLeft;


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

    //吹っ飛ばす方向
    private Vector3 _smashDirection;

    [SerializeField]
    private Sprite[] _arrowImages;
    [SerializeField]
    private Sprite[] _powerImages;
    [SerializeField]
    private GameObject _arrowImageObject;
    private UnityEngine.UI.Image _arrowImage;

    //プレイヤーのゲージ
    private PlayerGage _playerGage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = INITIAL_SPEED;
        _playerInput = GetComponent<PlayerInput>().currentActionMap;
        _rb = GetComponent<Rigidbody>();
        _playerAttack = _attackColl.GetComponent<PlayerAttack>();
        ChangeState(PlayerStateID.Idle);
        _arrowImage = _arrowImageObject.GetComponent<UnityEngine.UI.Image>();
        SetArrowActive(false);
        if(_playerGage = GetComponent<PlayerGage>())
        _playerGage.SetGageActive(false);
        //プレイヤーの攻撃を無効
        GetAttackColl().SetActive(false);

        _canBack = true;
        _canFront = true;
        _canLeft = true;
        _canRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //現在の状態の更新
        _currentState.StateUpdate();
        
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        // 攻撃状態かつアニメーションが待機状態だったら強制的に待機状態にする
        if (_anim.GetInteger("PlayerState") == 3 && (info.IsName("Armature|idle") || info.IsName("Armature|run")))
        {
            ChangeState(PlayerStateID.Idle);
        }

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

    //ゲッターセッター================================================-

    //スピード
    public void SetSpeed(float speed)
    { 
        _speed = speed;
    }
    public float GetSpeed()
    {
        return _speed;
    }

    //アタックコライダー
    public GameObject GetAttackColl()
    {
        return _attackColl;
    }

    //パワー
    public int GetPower()
    {
        return _playerAttack.GetPower();
    }

    public void SetPower(int power)
    {
        _playerAttack.SetPower(power);
    }

    //アニメーター
    public Animator GetAnimator()
    {
        return _anim;
    }

    //入力方向
    public Vector2 GetInputMove()
    {
        //入力から進行方向を取得
        Vector2 direction = _playerInput["Move"].ReadValue<Vector2>();
        return new Vector2(direction.x, direction.y);
    }

    //うしろに移動できるか
    public bool GetCanBack()
    {
        return _canBack;
    }
    public bool GetCanFront()
    {
        return _canFront;
    }
    public bool GetCanLeft()
    {
        return _canLeft;
    }
    public bool GetCanRight()
    {
        return _canRight;
    }


    //最後に向いていた方向
    public Vector3 GetLastDirection()
    {
        return new Vector3(_lastDirection.x, _lastDirection.y, _lastDirection.z);
    }
    public void SetLastDirection(Vector3 lastDirection)
    {
        _lastDirection = lastDirection;
    }

    //状態
    public void SetState(PlayerState playerState, Player obj)
    {

        if (_currentState != null)
        {
            _currentState.StateFinalize();
            _currentState = null;
        }
        _currentState = playerState;
        _currentState.Initialize(obj);
    }

    //モデルの回転
    public void SetModelRotation(Quaternion rot)
    {
        _modelRot.localRotation = rot;
    }


    //吹っ飛ばす方向
    public void SetSmashDirection(Vector3 smashDirection)
    {
        _smashDirection = smashDirection;
        //攻撃当たり判定オブジェクトにも
        _playerAttack.SetSmashDirection(_smashDirection);
    }


    //矢印
    public void SetArrowActive(bool active)
    {
        if(_arrowImage)
        _arrowImage.gameObject.SetActive(active);
    }

    public void SetArrowImage(int num)
    {
        num = Math.Clamp(num, 0, _arrowImages.Length - 1);
        _arrowImage.sprite = _arrowImages[num];
    }

    public int GetArrowImagesNum()
    {
        return _arrowImages.Length;
    }

    //ゲージ
    public PlayerGage GetGage()
    {
        return _playerGage;
    }

    //攻撃
    public PlayerAttack GetPlayerAttack()
    {
        return _playerAttack;
    }

    //=======================================================================----

    public void OnCollisionStay(Collision collision)
    {
        //後ろの壁とあたっている間は後ろへの移動を禁止
        if(collision.gameObject.CompareTag("BackWall"))
        {
            _canBack = false;
        }
        else if (collision.gameObject.CompareTag("FrontWall"))
        {
            _canFront = false;
        }

        if (collision.gameObject.CompareTag("RightWall"))
        {
            _canRight = false;
        }
        else if (collision.gameObject.CompareTag("LeftWall"))
        {
            _canLeft = false;
        }

    }
    public void OnCollisionExit(Collision collision)
    {
        //後ろの壁とあたっている間は後ろへの移動を禁止
        if (collision.gameObject.CompareTag("BackWall"))
        {
            _canBack = true;
        }
        if (collision.gameObject.CompareTag("FrontWall"))
        {
            _canFront = true;
        }

        if (collision.gameObject.CompareTag("RightWall"))
        {
            _canRight = true;
        }
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            _canLeft = true;
        }

    }


    public void PlayerMove(Vector3 velocity)
    {
        _rb.MovePosition(_rb.position + velocity);
        
    }

}
