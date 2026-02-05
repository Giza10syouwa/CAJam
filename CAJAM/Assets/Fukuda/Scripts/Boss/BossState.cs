using UnityEngine;

public class BossState 
{
    //è„éi
    private Boss _pBoss;

    public virtual void StateUpdate()
    {
    }
    public virtual void Initialize(Boss boss)
    {
        _pBoss = boss;
    }

    public Boss GetBoss() { return _pBoss; }
    public void SetBoss(Boss boss) { _pBoss = boss; }

}
