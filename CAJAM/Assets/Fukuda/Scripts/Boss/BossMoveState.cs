using UnityEngine;

public class BossMoveState : BossState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StateUpdate()
    {
        GetBoss().transform.position += GetBoss().transform.forward * Time.deltaTime * GetBoss().GetSpeed();
    }
}
