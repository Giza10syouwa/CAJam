using Unity.VisualScripting;
using UnityEngine;

public class Dynamite : SmashObject
{
    //îöíeÇÃÉpÉèÅ[
    private float _power;

    private Rigidbody _rb;

    [SerializeField]
    private Vector3 _vec;

    private float _timer;

    [SerializeField]
    private PlayerAttack _attack;

    private Vector3 _targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _power = 0.0f;
        _timer = 0.0f;
    }

    public override void OnHPLessZero()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb)
        {
            //gameObject.layer = LayerMask.NameToLayer("SmashObject");
            _rb.isKinematic = false;
            _power = (float)GetLastTakePower() * 30.0f;
            _vec = GetSmashDirection();
            //êÅÇ¡îÚÇŒÇ∑
            _rb.AddForce(_vec * (float)GetLastTakePower() * 10.0f, ForceMode.Impulse);

        }
    }

    public override void OnTakeDamage(int damage)
    {
       
    }

    public override void SmashObjectUpdate()
    {
        if(_rb)
        _timer += Time.deltaTime;
        if (_timer >= 4.0f) Explod();
    }

    private void OnCollisionEnter(Collision collision)
    {
        SmashObject expObj = collision.gameObject.GetComponent<SmashObject>();
        //Ç‡ÇµîöîjëŒè€ÇæÇ¡ÇΩÇÁ
        if (expObj)
        {
            _targetPos = expObj.transform.position;
            //expObj.Explod((expObj.transform.position - transform.position) * _power + new Vector3(0.0f,_power / 10.0f,0.0f));
            Explod();

        }

    }

    private void Explod()
    {
        if (_attack)
        {
            _attack.gameObject.SetActive(true);
            _attack.gameObject.transform.parent = null;
            _attack.gameObject.GetComponent<DestroyByTime>().TimerStart();
            _attack.SetSmashDirection((_targetPos - transform.position) + new Vector3(0.0f, _power / 10.0f, 0.0f));
            _attack.SetPower(2 + (int)_power);
            _attack.gameObject.transform.localScale += new Vector3(_power / 30.0f, _power / 30.0f, _power / 30.0f) * 1.5f;
            _attack.SetEffectScale(_attack.gameObject.transform.localScale.x / 10.0f);
        }
        GameObject.Destroy(gameObject);
    }


}
