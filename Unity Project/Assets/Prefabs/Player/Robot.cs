using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public static GameObject instance;

    public static float HP = 1;
    public static float MAXHP = 100;
    void Awake()
    {
        instance = this.gameObject;
        HP = MAXHP;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.name == "ClearPoint")
        {
            GameManager.instance.Clear();
        }
    }

}
