using UnityEngine;

public class BreakWall : SmashObject
{
    public float duration = 0.0f;
    public float amplitude = 0.0f;

    bool shakeFlag;
    float shakeTimer; 
    Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         shakeFlag = false;
        startPos = transform.localPosition;
    }




    //Update普通に
    public override void SmashObjectUpdate()
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

    //ダメージ受けたとき
    public override void OnTakeDamage(int damage)
    {
        shakeFlag = true;
    }

    //死んだとき
    public override void OnHPLessZero()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("SmashObject");
        rb.constraints = RigidbodyConstraints.None;


        // 回転させる
        //rb.AddTorque(Random.onUnitSphere * 50f, ForceMode.Impulse);      
        
        //吹っ飛ばす
        
        rb.AddForce(GetSmashDirection() * 50.0f, ForceMode.Impulse);
    }





}
