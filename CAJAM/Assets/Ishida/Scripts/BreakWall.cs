using UnityEngine;

public class BreakWall : SmashObject
{
    public float duration = 0.0f;
    public float amplitude = 0.0f;

    bool shakeFlag;
    bool canShake;
    float shakeTimer; 
    Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         shakeFlag = false;
        canShake = true;
        startPos = transform.localPosition;
    }




    //Update普通に
    public override void SmashObjectUpdate()
    {

        if (canShake)
        {
            if (shakeFlag)
            {
                shakeTimer += Time.deltaTime;
                if (shakeTimer < duration)
                {
                    float x = Random.Range(-1f, 1f) * amplitude;
                    float y = Random.Range(-1f, 1f) * amplitude;
                    transform.localPosition = startPos + new Vector3(x, y, 0);
                }
                else
                { // 揺れ終了
                    shakeFlag = false;
                    shakeTimer = 0f;
                    transform.localPosition = startPos;
                }
            }

        }

    }

    //ダメージ受けたとき
    public override void OnTakeDamage(int damage)
    {

        if (GetHP() <= 0)
        {
            canShake = false;
            shakeFlag = false;
        }
        else
        {
            shakeFlag = true;
        }

            
    }

    //死んだとき
    public override void OnHPLessZero()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        rb.WakeUp();
        Debug.Log("SmashDir = " + GetSmashDirection());
        gameObject.layer = LayerMask.NameToLayer("SmashObject");
        Vector3 dir = GetSmashDirection(); 


        // 回転させる
        rb.AddTorque(Random.onUnitSphere * 25f , ForceMode.Impulse);
        //吹っ飛ばす
        rb.AddForce((dir/*+ new Vector3(0,0,5)*/) * 30.0f, ForceMode.Impulse);


        

    }





}
