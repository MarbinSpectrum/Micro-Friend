using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class HPBar : MonoBehaviour
{
    Image HPBAR;
    float time = 0;


    private void Awake()
    {
        HPBAR = GetComponent<Image>();
    }

    void Update()
    {
        HPBAR.fillAmount = Robot.HP / Robot.MAXHP;
        time += Time.deltaTime;
        if(time > 1)
        {
            time = 0;
            Robot.HP--;
        }

    }
}
