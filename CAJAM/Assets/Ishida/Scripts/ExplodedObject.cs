using UnityEngine;

public class ExplodedObject : SmashObject
{
    private Rigidbody _rb;
    private float _timer;

    public override void SmashObjectUpdate()
    {
        if (GetHP() <= 0)
        {
            _timer += Time.deltaTime;
            if (_timer >= 4.0f)
            {
                GameObject.Destroy(gameObject);
            }
        }

    }

    public override void OnHPLessZero()
    {
        Explod(GetSmashDirection() * GetLastTakePower() * 30.0f);
    }

    public override void OnTakeDamage(int damage)
    {
       
    }

    public void Explod(Vector3 burstVelocity)
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb)
        {
            _rb.isKinematic = false;
            _rb.AddForce(burstVelocity,ForceMode.Impulse);
        }
    }

}
