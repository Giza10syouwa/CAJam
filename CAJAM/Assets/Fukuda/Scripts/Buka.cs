using UnityEngine;

public class Buka : SmashObject
{

    [SerializeField]
    private Animator _anim;

    private int _animID;

    private float _time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animID = 0;
        if (_anim)
            _anim.SetInteger("ID", _animID);


    }

    // Update is called once per frame
    void Update()
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
        //êÅÇ¡îÚÇŒÇ∑
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 4.0f, ForceMode.Impulse);
        rb.AddTorque(Vector3.Cross(GetSmashDirection(), -Vector3.up) * (float)GetLastTakePower() * 40.0f, ForceMode.Impulse);

        _animID = 1;
        if (_anim)
            _anim.SetInteger("ID", _animID);
    }
    public override void SmashObjectUpdate()
    {
        if (GetHP() <= 0)
        {
            _time += Time.deltaTime;
            if (_time >= 5.0f)
            {
                GameObject.Destroy(gameObject);
            }
        }

    }


}
