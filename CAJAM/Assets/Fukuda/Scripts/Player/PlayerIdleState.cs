using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{

    private bool _isLastPress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void StateUpdate()
    {

        if(GetPlayer().GetInputMove().magnitude > 0.2f)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Move);
        }

        if(Keyboard.current.spaceKey.isPressed)
        {
            GetPlayer().ChangeState(Player.PlayerStateID.Chage);
        }
    }

}
