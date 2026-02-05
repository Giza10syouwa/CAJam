using Unity.VisualScripting;
using UnityEngine;

public class OfficialPapers : SmashObject
{
    //爆弾のパワー
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
        //吹っ飛ばす
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 4.0f, ForceMode.Impulse);

    }
    public override void SmashObjectUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
       Boss boss = collision.gameObject.GetComponent<Boss>();
        //もし上司とぶつかったらスーツを金ぴかに
        if (boss)
        {
            ScoreUP();
            GameObject.Destroy(gameObject);
        }

    }


   
}
