using UnityEngine;

public class GoldPants : SmashObject
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
        //êÅÇ¡îÚÇŒÇ∑
        rb.AddForce(GetSmashDirection() * (float)GetLastTakePower() * 10.0f, ForceMode.Impulse);

    }
    public override void SmashObjectUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Ç‡Çµè„éiÇ∆Ç‘Ç¬Ç©Ç¡ÇΩÇÁÉXÅ[ÉcÇã‡Ç“Ç©Ç…
        if (collision.gameObject.CompareTag("Boss"))
        {
            ScoreUP();

            collision.gameObject.GetComponent<Boss>().GoldPants();
            GameObject.Destroy(gameObject);
        }

    }

}
