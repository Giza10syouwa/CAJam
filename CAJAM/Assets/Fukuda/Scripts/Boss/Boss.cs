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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeState(BossStateID.Move);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        int layerMask = ~LayerMask.GetMask("Player","SmashObject");
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
    public void SetState(BossState bossState, Boss obj)
    {
        if (_currentState != null)
        {
            GameObject.Destroy(_currentState);
        }
        _currentState = bossState;
        _currentState.Initialize(obj);
    }

}
