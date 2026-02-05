using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{

    private bool _isLastPress;

    public override void Initialize(Player player)
    {
        SetPlayer(player);
        //プレイヤーの攻撃を無効
        //GetPlayer().GetAttackColl().SetActive(false);

        ////矢印無効
        //GetPlayer().SetArrowActive(false);
        ////ゲージ無効
        //if (GetPlayer().GetGage())
        //    GetPlayer().GetGage().SetGageActive(false);


    }


    public override void StateUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Chage);
        }

        if (GetPlayer().GetInputMove().magnitude > 0.2f)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Move);
        }

    }

}
