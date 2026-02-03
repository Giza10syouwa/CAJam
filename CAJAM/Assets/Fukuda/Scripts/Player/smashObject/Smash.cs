using UnityEngine;

public class Smash : SmashObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void OnTakeDamage(int damage)
    {

    }
    public override void OnHPLessZero()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("SmashObject");
        rb.constraints = RigidbodyConstraints.None;
        //êÅÇ¡îÚÇŒÇ∑
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 5.0f, ForceMode.Impulse);
    }
    public override void SmashObjectUpdate()
    {

    }

}
