using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //プレイヤーのポインタ
    private Player _pPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void StateUpdate()
    {
    }


    public void Initialize(Player player)
    {
        _pPlayer = player;
    }

    public Player GetPlayer() { return _pPlayer; }

}
