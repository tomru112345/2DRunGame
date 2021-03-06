using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public GameObject Plane;
    // public GameObject ThirdPersonCharactor;

    GameObject[] step = new GameObject[10];
    float speed = 12;
    float disappear = -10;
    float respawn = 30;
    // int dis_num = 0;

    // float default_y = -5;
    public void addSpeed()
    {
        speed++;
    }
    void Start()
    {
        for (int i = 0; i < step.Length; i++)
        {
            step[i] = Instantiate(Plane, new Vector3(4 * i, 0, 0), Quaternion.identity);
            step[i].tag = "Ground";
            step[i].transform.parent = GameObject.Find("GroundList").transform;
            if (i % 10 == 5 || i % 10 == 9 || i % 10 == 6)
            {
                step[i].SetActive(false);
            }
        }
        // Instantiate(ThirdPersonCharactor, new Vector3(8, 5, 0), Quaternion.identity);
    }

    void Update()
    {
        for (int i = 0; i < step.Length; i++)
        {
            step[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (step[i].gameObject.transform.position.x < disappear)
            {
                // step[dis_num].SetActive(true);
                ChangeScale(i);
                step[i].gameObject.transform.position = new Vector3(respawn, 0, 0);
                // dis_num = Random.Range(1, 9);
                // step[dis_num].SetActive(false);
            }
        }

    }
    void ChangeScale(int i)
    {
        int x = (i + 9) % 10; //(i + 9) を 10 で割った余り
        if (step[x].transform.localScale.y == 0.5)
        {
            step[i].transform.localScale = step[x].transform.localScale + new Vector3(0, Random.Range(0, 2), 0);
        }
        else if (step[x].transform.localScale.y >= 10)
        {
            step[i].transform.localScale = step[x].transform.localScale + new Vector3(0, Random.Range(-1, 1), 0);
        }
        else
        {
            step[i].transform.localScale = step[x].transform.localScale + new Vector3(0, Random.Range(-1, 2), 0);
        }
    }
}
