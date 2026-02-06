using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Boss : MonoBehaviour
{
    //ボスの状態のID
    public enum BossStateID
    {
        None = -1,
        Idle,
        Move,
    };

    //ボスのIDの保持
    private BossStateID _currentStateID;

    //ボスの状態
    private BossState _currentState;

    //アニメーション
    [SerializeField]
    private Animator _anim;

    //現在の速度
    private float _speed;

    [SerializeField]
    private float INITIAL_SPEED;

    //今所属しているステージ
    [SerializeField]
    private Stage _stage;

    //リジッドボディ
    private Rigidbody _rb;


    //鞄オブジェクト
    [SerializeField]
    private GameObject _bagObject;

    //スーツのレンダラー
    [SerializeField]
    private SkinnedMeshRenderer _suitRenderer;

    //ズボンのレンダラー
    [SerializeField]
    private SkinnedMeshRenderer _pantsRenderer;


    //金マテリアル
    [SerializeField]
    private Material _gold;

    private bool _cantPlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cantPlay = true;
        ChangeState(BossStateID.Move);
        _speed = INITIAL_SPEED;
        _rb = GetComponent<Rigidbody>();
        _bagObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(_cantPlay)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        int layerMask = ~LayerMask.GetMask("Player","SmashObject","Clear");
        if (Physics.Raycast(ray, out hit, 2.0f,layerMask))
        {
            Debug.Log("目の前に何かある");
            ChangeState(Boss.BossStateID.Idle);
        }
        else
        {
            Debug.Log("目の前に何もない");
            ChangeState(Boss.BossStateID.Move);

        }

        _currentState.StateUpdate();
    }

    


    public void ChangeState(BossStateID id)
    {
        //指定されたIDに合わせて状態を遷移させる
        _currentStateID = id;

        _anim.SetInteger("BossState", (int)_currentStateID);

        if (id == BossStateID.Idle)
        {
            SetState(new BossIdleState(), this);
        }
        if (id == BossStateID.Move)
        {
            SetState(new BossMoveState(), this);
        }

    }

    public void SetZeroPos()
    {
        transform.position = Vector3.zero;
    }

    public void SetState(BossState bossState, Boss obj)
    {
        if (_currentState != null)
        {
            _currentState = null;
        }
        _currentState = bossState;
        _currentState.Initialize(obj);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Clear"))
        {
            _speed = 0.0f;
            _stage.GameClear();
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void AddSpeed(float speed)
    {
        _speed += speed;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void Move(Vector3 velocity)
    {
        _rb.MovePosition(_rb.position + velocity);
    }

    //バッグを有効
    public void BagActive()
    {
        _bagObject.SetActive(true);
    }

    //スーツのマテリアルを金ぴかに
    public void GoldSuit()
    {
        _suitRenderer.SetMaterials(new List<Material>{ _gold });
        Debug.Log("gold");
    }
    public void GoldPants()
    {
        _pantsRenderer.SetMaterials(new List<Material> { _gold });
        Debug.Log("gold");
    }

    public void CanPlay()
    {
        _cantPlay = false;
    }

}
