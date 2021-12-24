using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10f;
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
	[SerializeField] public GameObject timerText;
    [SerializeField] public GameObject GameOverText;
    [SerializeField] public GameObject QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
		timerText.GetComponent<Text>().text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
        GameOverText.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
		if(seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}
		//　値が変わった時だけテキストUIを更新
		if((int)seconds != (int)oldSeconds && !isGameOver) {
			timerText.GetComponent<Text>().text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
		}
		oldSeconds = seconds;

        if (JumpCount <= 1)//着地しているとき
        {
            if(Input.GetKeyDown("space"))
            {
                JumpCount++;//  isGroundをfalseにする
                myRigidbody.AddForce(Vector3.up * upForce); //上に向かって力を加える
            }
        }

        if (myRigidbody.transform.position.y < -1){
            //ゲームオーバー
            isGameOver = true;
            GameOverText.GetComponent<Text>().text = timerText.GetComponent<Text>().text;
            GameOverText.SetActive(true);
            timerText.SetActive(false);
            QuitButton.SetActive(true);
        }

        //ゲームオーバー状態で画面がクリックされたとき
        // if(isGameOver && Input.GetKeyDown("space"))
        // {
        //     Restart();
        // }
    }

    void OnCollisionEnter(Collision other) //地面に触れた時の処理
    {
        if (other.gameObject.tag == "Ground") //Groundタグのオブジェクトに触れたとき
        {
            JumpCount = 0; //isGroundをtrueにする
        }
    }

    //シーンを再読み込みする
    // private void Restart()
    // {
    //     // Sceneの読み直し
    //     SceneManager.LoadScene("TitleScene");
    // }
}
