using UnityEngine;

public class BossState : MonoBehaviour
{
    //è„éi
    private Boss _pBoss;

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
    public virtual void Initialize(Boss boss)
    {
        _pBoss = boss;
    }

    public Boss GetBoss() { return _pBoss; }
    public void SetBoss(Boss boss) { _pBoss = boss; }

}
