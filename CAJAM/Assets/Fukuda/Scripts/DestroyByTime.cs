using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    private bool _countF;

    private float _time;

    [SerializeField]
    private float DESTROY_TIME;

    // Start i
    // s called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FixedUpdate()
    {
        if(_countF)
        {
            _time += Time.deltaTime;
            if(_time >= DESTROY_TIME)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void TimerStart()
    {
        _countF = true;
        _time = 0;
    }
}
