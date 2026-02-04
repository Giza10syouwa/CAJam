using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChageState : PlayerState
{
    private Vector3 _lastDirection;

    private float _chagePower;

    private float _time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public override void Initialize(Player player)
    {
        SetPlayer(player);
        _chagePower = 0.0f;
        _lastDirection = GetPlayer().GetLastDirection();
        GetPlayer().SetPower((int)Mathf.Floor(_chagePower));
        _time = 0.0f;
        GetPlayer().SetArrowActive(true);
        GetPlayer().GetGage().SetGageActive(true);

    }

    public override void StateUpdate()
    {
        //入力から進行方向を取得
        Vector2 direction = GetPlayer().GetInputMove();
        direction = new Vector2(direction.x, direction.y);

        if (direction.magnitude <= 0.0f)
        {
            direction = new Vector2(_lastDirection.x,_lastDirection.z);
        }

        //正規化
        direction.Normalize();

        //XZ平面上の進行方向をきめる
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.y);

        //現在のモデルの向く方向をきめる
        Vector3 currentModelDirection = Vector3.Lerp(_lastDirection, directionXZ, Time.deltaTime * 5.0f);

        //モデルの回転
        GetPlayer().SetModelRotation(Quaternion.LookRotation(currentModelDirection));

        if(GetPlayer().GetGage())
        GetPlayer().GetGage().SetPos(GetPlayer().transform.position + -currentModelDirection * 2.0f);

        //向きを更新
        _lastDirection = currentModelDirection;
        
        //パワーを上昇
        _chagePower += Time.deltaTime;


        //プレイヤーに設定
        GetPlayer().SetPower((int)Mathf.Floor(_chagePower));

        _time += Time.deltaTime * 3.0f;
        GetPlayer().SetArrowImage((int)_time % GetPlayer().GetArrowImagesNum());


        //スペースキーが押されていなかった時
        if (!Keyboard.current.spaceKey.isPressed)
        {
            //吹っ飛ばす方向を指定
            GetPlayer().SetSmashDirection(currentModelDirection);

            //攻撃状態に遷移させる
            GetPlayer().ChangeState(Player.PlayerStateID.Attack);
        }

    }
}
