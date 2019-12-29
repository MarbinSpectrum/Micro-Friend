using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public GameObject bullet;

    public float speed = 0.03f;
    Vector3 targetPos;
    bool attackSW;
    float time = 60;
    public float cooltime = 15;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Robot")
            attackSW = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name == "Robot")
            attackSW = false;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > cooltime)
        {
            if (attackSW)
            {
                GameObject emp = Instantiate(bullet,transform.position, Quaternion.identity);
                emp.GetComponent<bulletControll>().speed = speed;
                Vector3 dic = Robot.instance.transform.position - transform.position;
                dic = dic.normalized;
                emp.GetComponent<bulletControll>().dic = dic;
                time = 0;
            }
        }
        bullet.transform.position += targetPos * speed;
    }
}
