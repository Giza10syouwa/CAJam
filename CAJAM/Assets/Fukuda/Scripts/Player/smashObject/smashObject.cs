using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SmashObject : MonoBehaviour
{
    [SerializeField]
    private int _hp;

    //‚Á”ò‚Ô•ûŒü
    private Vector3 _smashDirection;

    //ÅŒã‚Éó‚¯‚½UŒ‚—Í
    private int _lastTakePower;

    [SerializeField]
    private string _scoreStr;
    [SerializeField]
    private int _score;
    [SerializeField]
    private bool _isScoreEnable;

    private bool _isBreak = false;

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
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
            playerAttack.Hit();
            //UŒ‚—Í•ª‘Ì—Í‚ğŒ¸‚ç‚·
            int power = playerAttack.GetPower();
            _hp -= power;
            OnTakeDamage(power);
            if(_hp <= 0 && !_isBreak)
            {
                _isBreak = true;
                _lastTakePower = power;
                //ƒvƒŒƒCƒ„[‚Ì‚Á”ò‚Î‚µ•ûŒü‚ğæ“¾
                _smashDirection = playerAttack.GetSmashDirection();

                if(_isScoreEnable)
                Score.Instance.AddScore(_score, _scoreStr);


                //‰ó‚³‚ê‚½‚Ìˆ—‚ğ‚·‚é
                OnHPLessZero();
            }
        }
    }

    public Vector3 GetSmashDirection()
    {
        return _smashDirection;
    }

    public int GetLastTakePower()
    {
        return _lastTakePower;
    }

    public int GetHP()
    {
        return _hp;
    }

    public void ScoreUP()
    {
        Score.Instance.AddScore(_score, _scoreStr);
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
