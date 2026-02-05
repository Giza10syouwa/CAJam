using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{

    private bool _isLastPress;

    public override void StateUpdate()
    {

        if(GetPlayer().GetInputMove().magnitude > 0.2f)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Move);
        }

        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Chage);
        }
    }

}
