using UnityEngine;

public class Thief : SmashObject
{
    private Vector3 _pos;

    private float _posZ;

    [SerializeField]
    private Animator _anim;

    private int _animID;

    private float _time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _time = 0.0f;
        _pos = gameObject.transform.position;
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
        //‚Á”ò‚Î‚·
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 4.0f, ForceMode.Impulse);
        rb.AddTorque(Vector3.Cross(GetSmashDirection(), -Vector3.up) * (float)GetLastTakePower() * 40.0f, ForceMode.Impulse);

        _animID = 1;
        if (_anim)
            _anim.SetInteger("ID", _animID);
    }
    public override void SmashObjectUpdate()
    {
        if (GetHP() > 0)
        {
            _posZ = gameObject.transform.position.z;
            _posZ -= Time.deltaTime * 5.0f;
            gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,_posZ);
        }
        else
        {
            _time += Time.deltaTime;
            if(_time >= 5.0f)
            {
                GameObject.Destroy(gameObject);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BackWall") && GetHP() > 0)
        {
            Debug.Log("“D–_");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BackWall") && GetHP() > 0)
        {
            ScoreUP();
            Debug.Log("“D–_");

        }

    }
}
