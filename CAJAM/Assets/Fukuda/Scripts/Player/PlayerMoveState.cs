using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerState
{

    private Vector3 _lastDirection;

    public override void StateUpdate()
    {
        //入力から進行方向を取得
        Vector2 direction = GetPlayer().GetInputMove();
        direction = new Vector2(direction.x, direction.y);

        //もし入力が零だったら待機に戻す
        if(direction.magnitude <= 0.0f)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Idle);
        }

        //もし後ろに下がったらダメならマイナスへの移動をなくす
        if (!GetPlayer().GetCanBack())
        {
            direction.y = Mathf.Clamp(direction.y, 0.0f, 1.0f);
        }
        else if(!GetPlayer().GetCanFront())
        {
            direction.y = Mathf.Clamp(direction.y, -1.0f, 0.0f);
        }
        if (!GetPlayer().GetCanRight())
        {
            direction.x = Mathf.Clamp(direction.x, -1.0f, 0.0f);
        }
        else if (!GetPlayer().GetCanLeft())
        {
            direction.x = Mathf.Clamp(direction.x, 0.0f, 1.0f);
        }



        //正規化
        direction.Normalize();

        //XZ平面上の進行方向をきめる
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.y);

        //現在のモデルの向く方向をきめる
        Vector3 currentModelDirection = Vector3.Lerp(_lastDirection, directionXZ, Time.deltaTime * 5.0f);

        //モデルの回転
        GetPlayer().SetModelRotation(Quaternion.LookRotation(currentModelDirection));

        _lastDirection = currentModelDirection;

        GetPlayer().SetLastDirection(_lastDirection);

        //入力方向とスピードから速度取得
        Vector3 velocity = directionXZ * GetPlayer().GetSpeed() * Time.deltaTime;

        GetPlayer().PlayerMove(velocity);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Chage);
        }

    }
}
