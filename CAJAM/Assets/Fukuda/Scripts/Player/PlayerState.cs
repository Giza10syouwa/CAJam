using UnityEngine;

public class PlayerState 
{
    //プレイヤーのポインタ
    private Player _pPlayer;

    public virtual void StateUpdate()
    {
    }


    public virtual void Initialize(Player player)
    {
        _pPlayer = player;
    }

    public Player GetPlayer() { return _pPlayer; }
    public void SetPlayer(Player player) { _pPlayer = player; }
}
