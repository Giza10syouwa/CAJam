using UnityEngine;

public class Smash : SmashObject
{
    private float _time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _time = 0.0f;
    }

    public override void OnTakeDamage(int damage)
    {

    }
    public override void OnHPLessZero()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //gameObject.layer = LayerMask.NameToLayer("SmashObject");
        rb.constraints = RigidbodyConstraints.None;
        //êÅÇ¡îÚÇŒÇ∑
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 40.0f, ForceMode.Impulse);

    }
    public override void SmashObjectUpdate()
    {
        if(GetHP() <= 0)
        {
            _time += Time.deltaTime;
        }
        if(_time >= 5.0f)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
