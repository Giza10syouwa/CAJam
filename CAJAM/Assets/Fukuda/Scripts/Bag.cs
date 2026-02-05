using UnityEngine;

public class Bag : SmashObject
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        //吹っ飛ばす
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 4.0f, ForceMode.Impulse);

    }
    public override void SmashObjectUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //もし上司とぶつかったらバッグを有効に
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Boss>().BagActive();
            GameObject.Destroy(gameObject);
        }

    }
}
