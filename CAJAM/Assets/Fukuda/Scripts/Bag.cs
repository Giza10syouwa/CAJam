using UnityEngine;

public class Bag : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("SmashObject");
        if (collider.gameObject.CompareTag("Attack"))
        {
            PlayerAttack playerAttack = collider.GetComponent<PlayerAttack>();
            rb.constraints = RigidbodyConstraints.None;
            //êÅÇ¡îÚÇŒÇ∑
            rb.AddForce(playerAttack.GetSmashDirection() * 5.0f, ForceMode.Impulse);
        }
    }
}
