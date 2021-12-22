using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody myRigidbody;
    public float upForce = 400f; //上方向にかける力
    private int JumpCount = 0; //着地しているかどうかの判定

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (JumpCount <= 1)//着地しているとき
        {
            if(Input.GetKeyDown("space"))
            {
                JumpCount++;//  isGroundをfalseにする
                myRigidbody.AddForce(Vector3.up * upForce); //上に向かって力を加える
            }
        }
    }

    void OnCollisionEnter(Collision other) //地面に触れた時の処理
    {
        if (other.gameObject.tag == "Ground") //Groundタグのオブジェクトに触れたとき
        {
            JumpCount = 0; //isGroundをtrueにする
        }
    }
}
