using Unity.VisualScripting;
using UnityEngine;

public class MovingToTarget : MonoBehaviour
{
    //“’…‚·‚é—\’è‚ÌêŠ
    [SerializeField]
    private Vector3 _targetPosition;

    //‰ŠúˆÊ’u
    private Vector3 _initialPosition;

    private float _ratio;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _initialPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRatio(float ratio)
    {
        _ratio = Mathf.Clamp(ratio,0.0f,1.0f);

        transform.position =Vector3.Lerp(_initialPosition, _targetPosition, _ratio);

    }

}
