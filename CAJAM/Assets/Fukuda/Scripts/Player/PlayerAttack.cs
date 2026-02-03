using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //çUåÇóÕ
    private int _power;

    private Vector3 _smashDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPower()
    {
        return _power;
    }

    public void SetPower(int power)
    {
        _power = power;
    }

    public Vector3 GetSmashDirection()
    {
        return _smashDirection;
    }
    public void SetSmashDirection(Vector3 direction)
    {
        _smashDirection = direction;
    }

}
