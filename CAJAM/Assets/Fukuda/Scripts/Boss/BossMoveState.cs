using UnityEngine;

public class BossMoveState : BossState
{

    public override void StateUpdate()
    {
        GetBoss().Move(GetBoss().transform.forward * Time.deltaTime * GetBoss().GetSpeed());
    }
}
