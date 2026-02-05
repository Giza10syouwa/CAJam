using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public override void Initialize(Player player)
    {
        SetPlayer(player);
        //プレイヤーの攻撃を有効
        GetPlayer().GetAttackColl().SetActive(true);
        GetPlayer().SetArrowImage(GetPlayer().GetArrowImagesNum() - 1);

    }


    // Update is called once per frame
    public override void StateUpdate()
    {
        if (GetPlayer().GetGage())
        GetPlayer().GetGage().SetPos(GetPlayer().transform.position + -GetPlayer().GetPlayerAttack().GetSmashDirection() * 2.0f);


        AnimatorStateInfo info = GetPlayer().GetAnimator().GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Armature|swingattack") && info.normalizedTime >= 0.4303)
        {
            //プレイヤーの攻撃を無効
            GetPlayer().GetAttackColl().SetActive(false);

        }
        // "Run" アニメーションが終了したか
        if (info.IsName("Armature|swingattack") && info.normalizedTime >= 1.0f)
        {
            //プレイヤーの攻撃を無効
            GetPlayer().GetAttackColl().SetActive(false);

            //矢印無効
            GetPlayer().SetArrowActive(false);
            //ゲージ無効
            if (GetPlayer().GetGage())
            GetPlayer().GetGage().SetGageActive(false);

            GetPlayer().ChangeState(Player.PlayerStateID.Idle);
        }
    }
}
