using Unity.VisualScripting;
using UnityEngine;

public class OfficialPapers : SmashObject
{
    //”š’e‚Ìƒpƒ[
    private float _power;

    private Rigidbody _rb;

    //[SerializeField]
    private Vector3 _vec;

    private float _timer;

    bool _isActive = false;


    void Start()
    {

    }

    private void Update()
    {

    }

    public override void OnTakeDamage(int damage)
    {

    }
    public override void OnHPLessZero()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        gameObject.layer = LayerMask.NameToLayer("SmashObject");
        rb.isKinematic = false;
        //‚Á”ò‚Î‚·
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 4.0f, ForceMode.Impulse);

    }
    public override void SmashObjectUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
       Boss boss = collision.gameObject.GetComponent<Boss>();
        //‚à‚µãi‚Æ‚Ô‚Â‚©‚Á‚½‚çƒX[ƒc‚ğ‹à‚Ò‚©‚É
        if (boss)
        {
            Score.Instance.AddScore(200,"ãi‚É‘—Ş‚ğ“n‚·");
            GameObject.Destroy(gameObject);
        }

    }


   
}
