using UnityEngine;

public class PlayerAttackState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void StateUpdate()
    {
        AnimatorStateInfo info = GetPlayer().GetAnimator().GetCurrentAnimatorStateInfo(0);
        // "Run" アニメーションが終了したか
        if (info.IsName("Armature|swingattack") && info.normalizedTime >= 1.0f)
        {
            Debug.Log("a");
            GetPlayer().ChangeState(Player.PlayerStateID.Idle);
        }
    }
}
