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
            gameObject.layer = LayerMask.NameToLayer("SmashObject");
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
        ExplodedObject expObj = collision.gameObject.GetComponent<ExplodedObject>();
        //Ç‡ÇµîöîjëŒè€ÇæÇ¡ÇΩÇÁ
        if (expObj)
        {
            expObj.Explod((expObj.transform.position - transform.position) * _power + new Vector3(0.0f,_power / 10.0f,0.0f));
            Explod();

        }

    }

    private void Explod()
    {
        GameObject.Destroy(gameObject);

    }


}
