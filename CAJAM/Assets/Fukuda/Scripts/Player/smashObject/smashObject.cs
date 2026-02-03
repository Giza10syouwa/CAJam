using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SmashObject : MonoBehaviour
{
    [SerializeField]
    private int _hp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SmashObjectUpdate();
    }

    public void OnTriggerEnter(Collider other)
    {
        //‚à‚µUŒ‚‚Æ‚ ‚½‚Á‚½‚ç
        if(other.gameObject.CompareTag("Attack"))
        {
            //UŒ‚—Í•ª‘Ì—Í‚ğŒ¸‚ç‚·
            int power = other.GetComponent<PlayerAttack>().GetPower();
            _hp -= power;
            OnTakeDamage(power);
            if(_hp <= 0)
            {
                //‰ó‚³‚ê‚½‚Ìˆ—‚ğ‚·‚é
                OnHPLessZero();
            }
        }
    }

    public virtual void OnTakeDamage(int damage)
    {

    }

    public virtual void OnHPLessZero()
    {

    }

    public virtual void SmashObjectUpdate()
    {

    }

}
