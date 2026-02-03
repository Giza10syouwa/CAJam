using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChageState : PlayerState
{
    private Vector3 _lastDirection;

    private float _chagePower;

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
    }

    public override void StateUpdate()
    {
        //入力から進行方向を取得
        Vector2 direction = GetPlayer().GetInputMove();
        direction = new Vector2(direction.x, direction.y);

        if (direction.magnitude <= 0.0f)
        {
            direction = _lastDirection;
        }

        //正規化
        direction.Normalize();

        //XZ平面上の進行方向をきめる
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.y);

        //現在のモデルの向く方向をきめる
        Vector3 currentModelDirection = Vector3.Lerp(_lastDirection, directionXZ, Time.deltaTime * 5.0f);

        //モデルの回転
        GetPlayer().SetModelRotation(Quaternion.LookRotation(currentModelDirection));

        //向きを更新
        _lastDirection = currentModelDirection;
        
        //パワーを上昇
        _chagePower += Time.deltaTime;

        //プレイヤーに設定
        GetPlayer().SetPower((int)Mathf.Floor(_chagePower));

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
