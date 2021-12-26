using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterButtonScript : MonoBehaviour
{
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        // Sceneの読み直し
        SceneManager.LoadScene("GameScene");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnMouseEnter()
    {
        Button StartButton = GetComponent<Button>();    // 対象のボタン
        StartButton.animator.SetTrigger("Highlighted");
    }
}
