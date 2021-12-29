using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // public float speed = 5f;
    public Rigidbody myRigidbody;
    public float upForce = 200f; //上方向にかける力
    private int JumpCount = 0; //着地しているかどうかの判定
    //ゲームオーバー判定
    private bool isGameOver = false;
    [SerializeField] private int minute;
    [SerializeField] private float seconds;
    //　前のUpdateの時の秒数
    private float oldSeconds;
    //　タイマー表示用テキスト
    [SerializeField] private GameObject timerText;
    // [SerializeField] private GameObject GameOverText;
    private Animator animator;
    public GameObject mainCamera;


    float[,] Camera_set = new float[,] {
         {2.5f, 3.75f, -10f},
         {-5f, 15f, 0f}
         };
    float[,] Camera_rotate = new float[,] {
         {0f, 0f, 0f},
         {45f, 90f, 0f}
         };
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;
        timerText.GetComponent<Text>().text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        // GameOverText.GetComponent<Text>().text = "";
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if (seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;
            Vector3 pos = mainCamera.transform.position;

            if (minute >= 1 && seconds == 0)
            {
                PlaneScript Plane = new PlaneScript();
                Plane.addSpeed();
            }
            // Vevtor3 rot= mainCamera.Quaternion.eulerAngles;
            // Vector3 rot = mainCamera.transform.rotation;
            // mainCamera.transform.localPosition -= new Vector3((pos.x - Camera_set[minute % 2, 0]) / 100, (pos.y - Camera_set[minute % 2, 1]) / 100, (pos.z - Camera_set[minute % 2, 2]) / 100);
            mainCamera.transform.localPosition = new Vector3(Camera_set[minute % 2, 0], Camera_set[minute % 2, 1], Camera_set[minute % 2, 2]);
            // rot.x -= (rot.x - Camera_rotate[minute % 2, 0]) / 100;
            // rot.y -= (rot.y - Camera_rotate[minute % 2, 1]) / 100;
            // rot.z -= (rot.z - Camera_rotate[minute % 2, 2]) / 100;
            // mainCamera.transform.rotation -= Quaternion.Euler((rot.x - Camera_rotate[minute % 2, 0]) / 100, (rot.y - Camera_rotate[minute % 2, 1]) / 100, (rot.z - Camera_rotate[minute % 2, 2]) / 100);
            mainCamera.transform.localEulerAngles = new Vector3(Camera_rotate[minute % 2, 0], Camera_rotate[minute % 2, 1], Camera_rotate[minute % 2, 2]);
        }

        //　値が変わった時だけテキストUIを更新
        if ((int)seconds != (int)oldSeconds && !isGameOver && myRigidbody.transform.position.y > -1)
        {
            timerText.GetComponent<Text>().text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;

        if (JumpCount < 3)//着地しているとき
        {
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                animator.ResetTrigger("Jump");
                animator.enabled = false;
                JumpCount++;//  isGroundをfalseにする
                myRigidbody.AddForce(Vector3.up * upForce); //上に向かって力を加える
                animator.enabled = true;
                animator.SetTrigger("Jump");
            }
        }

        if (myRigidbody.transform.position.y < -30)
        {
            //ゲームオーバー
            isGameOver = true;
            SceneManager.LoadScene("ResultScene");
            // GameOverText.GetComponent<Text>().text = timerText.GetComponent<Text>().text;
            // GameOverText.SetActive(true);
            // timerText.SetActive(false);
            // QuitButton.SetActive(true);
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
