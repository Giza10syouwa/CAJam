using UnityEngine;

public class Thief : SmashObject
{
    private Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetHP() > 0)
        {
            pos.z -= Time.deltaTime;
            gameObject.transform.position = pos;
        }
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
        Score.Instance.AddScore(-100,"ìDñ_Çì¶ÇµÇΩ");
    }
    public override void SmashObjectUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
