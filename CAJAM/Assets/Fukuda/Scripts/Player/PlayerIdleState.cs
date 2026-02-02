using UnityEngine;

public class PlayerIdleState : PlayerState
{
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
    }

}
