using UnityEngine;

public class ExplodedObject : MonoBehaviour
{
    private Rigidbody _rb;
    private float _timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (_rb)
        {
            _timer += Time.deltaTime;
            if (_timer >= 4.0f)
            {
                GameObject.Destroy(gameObject);
            }
        }

    }

    public void Explod(Vector3 burstVelocity)
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb)
        {
            _rb.isKinematic = false;
            _rb.AddForce(burstVelocity,ForceMode.Impulse);
        }
    }

}
