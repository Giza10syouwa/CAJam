using UnityEngine;

public class PlayerAttackState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
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
        GetPlayer().GetGage().SetPos(GetPlayer().transform.position + -GetPlayer().GetPlayerAttack().GetSmashDirection());


        AnimatorStateInfo info = GetPlayer().GetAnimator().GetCurrentAnimatorStateInfo(0);
        // "Run" アニメーションが終了したか
        if (info.IsName("Armature|swingattack") && info.normalizedTime >= 1.0f)
        {
            //プレイヤーの攻撃を無効
            GetPlayer().GetAttackColl().SetActive(false);

            //矢印無効
            GetPlayer().SetArrowActive(false);
            //ゲージ無効
            GetPlayer().GetGage().SetGageActive(false);

            GetPlayer().ChangeState(Player.PlayerStateID.Idle);
        }
    }
}
