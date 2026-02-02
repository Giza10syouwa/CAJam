using UnityEngine;

public class Stage : MonoBehaviour
{
    //
    [SerializeField]
    private float _maxRatio;

    private float _time;

    [SerializeField]
    private float INITIAL_SPEED;

    private float _speed;

    //ÉQÅ[ÉÄÇÃÉJÉÅÉâ
    [SerializeField]
    private MovingToTarget _joshi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _time = 0.0f;
        _speed = INITIAL_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_time < _maxRatio)
        {
            _time += Time.deltaTime * _speed;
        }

        _joshi.SetRatio(_time / _maxRatio);
    }
}
